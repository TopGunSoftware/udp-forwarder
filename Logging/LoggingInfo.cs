
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
        /// The username of the user issuing the request,
        /// if any.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The response of the request: 200 (OK); 404 (NotFound) etc.
        /// </summary>
        public int ResponseStatusCode { get; set; }

        /// <summary>
        /// The IP address which issued the request.
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// A list of HTTP headers accompanying the request.
        /// </summary>
        public string Headers { get; set; }

    }

}