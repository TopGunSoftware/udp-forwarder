
using System;
using System.Text;
using System.Runtime.Serialization;

namespace UDPForwarder.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public class LoggingInfo
    {
        /// <summary>
        /// Database-generated primary key.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The date of the start of the request.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// The number of milliseconds that elapsed until the request
        /// had been processed.
        /// </summary>
        public long Elapsed { get; set; }

        /// <summary>
        /// The type of request (GET, POST, PUT etc.)
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// The URI of the request.
        /// </summary>
        public string UriAccessed { get; set; }

        /// <summary>
        /// The HTTP body of the request.
        /// </summary>
        public string RequestBody { get; set; }

        /// <summary>
        /// The username of the user issuing the request,
        /// if any.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The response of the request: 200 (OK); 404 (NotFound) etc.
        /// </summary>
        public int ResponseStatusCode { get; set; }

        /// <summary>
        /// TODO: is this any different from ResponseStatusCode?
        /// </summary>
        public string ResponseStatusMessage { get; set; }

        /// <summary>
        /// The IP address which issued the request.
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// A list of HTTP headers accompanying the request.
        /// </summary>
        public string Headers { get; set; }

        /// <summary>
        /// ToString is overridden, such that if we need to write
        /// an instance of this class to text file, we can do that
        /// in one statement.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("Start:");
            stringBuilder.Append(Start);
            stringBuilder.Append(Environment.NewLine);

            stringBuilder.Append("Elapsed: ");
            stringBuilder.Append(Elapsed);
            stringBuilder.Append(Environment.NewLine);

            stringBuilder.Append("HttpMethod: ");
            stringBuilder.Append(HttpMethod);
            stringBuilder.Append(Environment.NewLine);

            stringBuilder.Append("Uri: ");
            stringBuilder.Append(UriAccessed);
            stringBuilder.Append(Environment.NewLine);

            stringBuilder.Append("RequestBody: ");
            stringBuilder.Append(RequestBody);
            stringBuilder.Append(Environment.NewLine);

            stringBuilder.Append("UserName: ");
            stringBuilder.Append(UserName);
            stringBuilder.Append(Environment.NewLine);

            stringBuilder.Append("ResponseStatusCode: ");
            stringBuilder.Append(ResponseStatusCode);
            stringBuilder.Append(Environment.NewLine);

            stringBuilder.Append("ResponseStatusMessage: ");
            stringBuilder.Append(ResponseStatusMessage);
            stringBuilder.Append(Environment.NewLine);

            stringBuilder.Append("IpAddress: ");
            stringBuilder.Append(IpAddress);
            stringBuilder.Append(Environment.NewLine);

            stringBuilder.Append("Headers: ");
            stringBuilder.Append(Headers);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(Environment.NewLine);

            return stringBuilder.ToString();
        }
    }

}