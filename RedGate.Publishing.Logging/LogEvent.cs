using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedGate.Publishing.Logging
{
    public class LogEvent
    {
        public static LogEvent OfSeverity(Severity severity)
        {
            return new LogEvent().Severity(severity);
        }

        private readonly IDictionary<string, object> m_Values = new Dictionary<string, object>();

        public LogEvent Severity(Severity severity)
        {
            m_Values.Add("severity", severity.ToString().ToLowerInvariant());
            return this;
        }

        public LogEvent WithException(Exception exception)
        {
            var stackTrace = exception.StackTrace;
            m_Values.Add("message", exception.Message);
            m_Values.Add("exceptionClass", exception.GetType().ToString());
            m_Values.Add("stackTrace", stackTrace);
            m_Values.Add("sourceLocation", FirstLineOf(stackTrace));
            m_Values.Add("asString", exception.ToString());
            return this;
        }

        public LogEvent WithHttpRequest(HttpRequest request)
        {
            m_Values.Add("url", request.RawUrl);
            m_Values.Add("requestHeaders", request.Headers.AllKeys
                .Where(key => key.ToLowerInvariant() != "cookie")
                .ToDictionary(
                    key => key,
                    key => request.Headers[key]
                )
            );
            return this;
        }

        public IDictionary<string, object> ToDictionary()
        {
            return m_Values;
        }

        public LogEvent Add(string key, object value)
        {
            m_Values.Add(key, value);
            return this;
        }

        private static string FirstLineOf(string str)
        {
            return str.Substring(0, Math.Max(str.IndexOfAny(new[] { '\n', '\r' }), str.Length));
        }
    }
}
