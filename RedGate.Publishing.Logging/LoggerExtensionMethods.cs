using System;

namespace RedGate.Publishing.Logging
{
    public static class LoggerExtensionMethods
    {
        public static void TryWithLoggedExceptions<TException>(
            this ILogger logger, Action action) where TException : Exception
        {
            try
            {
                action();
            }
            catch (TException exception)
            {
                logger.LogException(Severity.Error, exception);
            }
        }

        public static void LogException(this ILogger logger, Severity severity, Exception exception)
        {
            logger.Log(new LogEvent().Severity(severity).WithException(exception));
        }
    }
}
