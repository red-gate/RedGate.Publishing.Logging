using Xunit;

namespace RedGate.Publishing.Logging.Tests
{
    public class InMemoryLoggerTests
    {
        [Fact]
        public void LoggedEventsCanBeRetrieved()
        {
            var logger = new InMemoryLogger();
            var logEvent = LogEvent.OfSeverity(Severity.Error);
            logger.Log(logEvent);

            Assert.Equal(1, logger.Events.Count);
            Assert.Equal(logEvent, logger.Events[0]);
        }
    }
}
