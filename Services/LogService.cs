using System;
using UDPForwarder.Logging;
using Newtonsoft.Json;

namespace UDPForwarder.Services
{
    public class LogService : IUsageLoggingService
    {
        private ITransportService transportService;
 
        public LogService(ITransportService _transportService)
        {
            transportService = _transportService;
        }

        public void Log(LoggingInfo info)
        {
            try
            {
                string jsonInfo = JsonConvert.SerializeObject(info);
                transportService.SendLog(jsonInfo);
                //TODO: This is for debug, remove
                System.IO.File.AppendAllText("C:\\CentrisTemp\\Log.txt", jsonInfo);
            }
            catch (Exception ex)
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

                // Try logging to the file system instead...
                //TODO: Handle exception
                System.IO.File.AppendAllText("C:\\Temp\\ApiUsageBackupLog.txt", msg);
                System.IO.File.AppendAllText("C:\\Temp\\ApiUsageBackupLog.txt", info.ToString());
            }
        }
    }
}