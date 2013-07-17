using System;
using Xunit;

namespace RedGate.Publishing.Logging.Tests
{
    public class LoggerExtensionMethodsTests
    {
        [Fact]
        public void LogExceptionSetsSeverity()
        {
            var logger = new InMemoryLogger();
            logger.LogException(Severity.Warning, GenerateException());

            var logEventFields = logger.Events[0].ToDictionary();
            Assert.Equal("warning", logEventFields["severity"]);
        }

        [Fact]
        public void LogExceptionSetsExceptionDetails()
        {
            var logger = new InMemoryLogger();
            logger.LogException(Severity.Warning, GenerateException());

            var logEventFields = logger.Events[0].ToDictionary();
            Assert.Equal("Oh no!", logEventFields["message"]);
        }

        private Exception GenerateException()
        {
            try
            {
                throw new Exception("Oh no!");
            }
            catch (Exception e)
            {
                return e;
            }
        }
    }
}
