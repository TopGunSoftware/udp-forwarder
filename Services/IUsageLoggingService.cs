using UDPForwarder.Logging;

namespace UDPForwarder.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUsageLoggingService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggingInfo"></param>
        void Log(LoggingInfo loggingInfo);
    }
}
