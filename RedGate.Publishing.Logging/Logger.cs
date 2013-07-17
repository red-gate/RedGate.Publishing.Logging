using System.Configuration;

namespace RedGate.Publishing.Logging
{
    public class Logger
    {
        public static ILogger CreateFromConfiguration()
        {
            var host = ConfigurationManager.AppSettings["LogstashTcpServer"];
            if (string.IsNullOrEmpty(host))
            {
                return new NullLogger();
            }
            else
            {
                var hostParts = host.Split(':');
                var hostname = hostParts[0];
                var port = int.Parse(hostParts[1]);
                return new LogstashTcpLogger(hostname, port);
            }
        }
    }
}
