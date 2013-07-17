using System.Collections.Generic;

namespace RedGate.Publishing.Logging
{
    public class InMemoryLogger : ILogger
    {
        private readonly IList<LogEvent> m_Events = new List<LogEvent>();

        public void Log(LogEvent logEvent)
        {
            m_Events.Add(logEvent);
        }

        public IList<LogEvent> Events
        {
            get { return m_Events; }
        }
    }
}
