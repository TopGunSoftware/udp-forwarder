using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using UDPForwarder.Logging;
using UDPForwarder.Services;
using Thinktecture.IdentityModel.Extensions;

namespace UDPForwarder.Filters
{
    public class LoggingHandler : DelegatingHandler
    {
        private readonly IUsageLoggingService _service;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="service"></param>
		public LoggingHandler(IUsageLoggingService service)
		{
			_service = service;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="innerHandler"></param>
		/// <param name="service"></param>
		public LoggingHandler(HttpMessageHandler innerHandler, IUsageLoggingService service)
			: base(innerHandler)
		{
			_service = service;
		}

        /// <summary>
        /// The main method, which gets called for each request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            // Log the request information
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var info = CreateLoggingInfoFromRequest(request);

            // Execute the request
            return base.SendAsync(request, cancellationToken).ContinueWith(task =>
            {
                var response = task.Result;
                // Extract the response logging info then persist the information
                stopWatch.Stop();
                info.Elapsed = stopWatch.ElapsedMilliseconds;
                LogResponseLoggingInfo(response, info);
                return response;
            }, cancellationToken);

        }

        private LoggingInfo CreateLoggingInfoFromRequest(HttpRequestMessage request)
        {
            var info = new LoggingInfo
            {
                Start = DateTime.Now,
                HttpMethod = request.Method.Method,
                UriAccessed = request.RequestUri.AbsoluteUri,
                IpAddress = HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : "0.0.0.0",
            };

            // Figure out the current user, if any
            var principal = request.GetClaimsPrincipal();
            if (principal != null)
            {
                if (principal.Identity != null)
                {
                    info.UserName = principal.Identity.Name;
                }
            }

            ExtractMessageHeadersIntoLoggingInfo(info, request.Headers.ToList());

            if (request.Content != null)
            {
                request.Content.ReadAsByteArrayAsync()
                    .ContinueWith(task =>
                    {
                        // TODO: we might want to make this more robust.
                        // As it stands, it is possible that the request
                        // has been dealt with and our actual logging method
                        // may have been executed, when we are finally able
                        // to write the RequestBody property.
                        info.RequestBody = Encoding.UTF8.GetString(task.Result);
                    });

            }
            return info;
        }

        private void LogResponseLoggingInfo(HttpResponseMessage response, LoggingInfo info)
        {
            info.ResponseStatusCode = (int)response.StatusCode;
            info.ResponseStatusMessage = response.ReasonPhrase;

            if (response.Content != null)
            {
                response.Content.ReadAsByteArrayAsync()
                    .ContinueWith(task =>
                    {
                        var responseMsg = Encoding.UTF8.GetString(task.Result);
                        info.ResponseBody = responseMsg;
                        _service.Log(info);
                    });

                return;
            }

            _service.Log(info);
        }

        private void ExtractMessageHeadersIntoLoggingInfo(LoggingInfo info, List<KeyValuePair<string, IEnumerable<string>>> headers)
        {
            var allHeaders = new StringBuilder();

            headers.ForEach(h =>
            {
                // Convert the header values into one long string from a series of IEnumerable<string> values so it looks for like a HTTP header
                var headerValues = new StringBuilder();

                if (h.Value != null)
                {
                    foreach (var hv in h.Value)
                    {
                        if (headerValues.Length > 0)
                        {
                            headerValues.Append(", ");
                        }
                        headerValues.Append(hv);
                    }
                }

                allHeaders.Append(string.Format("{0}: {1}", h.Key, headerValues.ToString()));
            });

            info.Headers = allHeaders.ToString();
        }

    }
}