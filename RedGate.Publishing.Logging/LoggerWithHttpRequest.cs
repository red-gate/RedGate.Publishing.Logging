using System.Web;

namespace RedGate.Publishing.Logging
{
    public class LoggerWithHttpRequest : ILogger
    {
        private readonly ILogger m_Logger;
        private readonly HttpRequest m_Request;

        public LoggerWithHttpRequest(ILogger logger, HttpRequest request)
        {
            m_Logger = logger;
            m_Request = request;
        }

        public void Log(LogEvent logEvent)
        {
            m_Logger.Log(logEvent.WithHttpRequest(m_Request));
        }
    }
}
