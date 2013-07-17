using System;
using System.IO;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace RedGate.Publishing.Logging
{
    public class LogstashTcpLogger : ILogger
    {
        private readonly string m_Hostname;
        private readonly int m_Port;

        public LogstashTcpLogger(string hostname, int port)
        {
            m_Hostname = hostname;
            m_Port = port;
        }

        public void Log(LogEvent logEvent)
        {
            Action logException = () =>
            {
                var eventFields = logEvent.ToDictionary();
                var json = JsonConvert.SerializeObject(eventFields);
                using (var client = new TcpClient(m_Hostname, m_Port))
                {
                    using (var writer = new StreamWriter(client.GetStream()))
                    {
                        writer.Write(json);
                    }
                }
            };
            logException.BeginInvoke(null, null);
        }
    }
}
