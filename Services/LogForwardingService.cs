using System;
using System.Web.Configuration;
using UDPForwarder.Logging;
using Newtonsoft.Json;

namespace UDPForwarder.Services
{
    /// <summary>
    /// The main service in the UDPForwarder library. LogService takes in an instance of a 
    /// transport service (default is UDP) and uses that service to forward logs to a specified server.
    /// This is built and tested with Logstash on the receiving end.
    /// </summary>
    public class LogForwardingService
    {
        private ITransportService transportService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_transportService"></param>
        public LogForwardingService(ITransportService _transportService)
        {
            transportService = _transportService;
        }

        /// <summary>
        /// Tries to forward the log to the transport service. If it is unsuccessful it will log 
        /// the error to a file specified in the Web.config (FallbackLogPath in AppSettings).
        /// </summary>
        /// <param name="info"></param>
        public void Log(LoggingInfo info)
        {
            try
            {
                string jsonInfo = JsonConvert.SerializeObject(info);
                transportService.SendLog(jsonInfo);

            }
            catch (Exception ex)
            {
                string logPath = WebConfigurationManager.AppSettings["FallbackLogPath"];
                if (logPath != null)
                {
                    var msg = ex.Message;
                    if (ex.InnerException != null)
                    {
                        msg += ex.InnerException.Message;
                        if (ex.InnerException.InnerException != null)
                        {
                            msg += ex.InnerException.InnerException.Message;
                        }
                    }

                    System.IO.File.AppendAllText(logPath, msg);
                    System.IO.File.AppendAllText(logPath, info.ToString());
                }
            }
        }
    }
}