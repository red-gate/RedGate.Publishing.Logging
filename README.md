# RedGate.Publishing.Logging

Log events, where an event is a set of key-value pairs.

## `LogEvent`

A `LogEvent` is a set of key-value pairs.
It has the following methods:

* `LogEvent Add(string key, string value)`:
  add a key-value pair to the event.

* `IDictionary<string, object> ToDictionary()`:
  convert the event into a dictionary.
  Useful for serialising the event.

* `LogEvent WithException(Exception exception)`:
  add the details of the exception to the event.
  Adds the following pairs:

  * `message`
  * `exceptionClass`
  * `stackTrace`
  * `sourceLocation`
  * `asString`

* `LogEvent WithHttpRequest(HttpRequest request)`:
  add the details of the HTTP request to the event.
  Adds the following pairs:

  * `url`
  * `requestHeaders` (excludes the `cookie` header)

## ILogger

Use an `ILogger` to log a `LogEvent` somewhere.
It has a single method:

* `void Log(LogEvent logEvent)`

`LoggerExtensionMethods` contains extension methods for `ILogger`:

* `void TryWithLoggedExceptions<TException>(
  this ILogger logger, Action action)
  where TException : Exception`:
  Invoke `action`.
  If it throws an exception of type `TException`,
  catch and log the exception.
  The exception is not re-thrown.

* `void LogException(this ILogger logger, Severity severity, Exception exception)`:
  Log an exception.

`ILogger` has several implementations.

### NullLogger

Does nothing.

### InMemoryLogger

Logs the events to an internal list
that can be retrieved via the `Events` property.

### LogstashTcpLogger

Logs the events using Logstack over TCP.
Must be constructed with the hostname and port of the Logstash TCP endpoint.

For instance, if a fragment of your Logstash configuration looks like this:

```
input {
    tcp {
        mode => "server"
        port => 59000
        format => "json"
        # Skipping other options for brevity
    }
}
```

Then you can build a logger like so:

```
new LogstashTcpLogger(hostname, 59000)
```

### LoggerWithHttpRequest

Adds HTTP request details to events before logging them with the underlying logger.
For instance:

```
// Assuming that request is an instance of HttpRequest
var logger = new LoggerWithHttpRequest(new InMemoryLogger(), request);

// ...

// This will log the logEvent with its existing key-value pairs
// as well as the values added by logEvent.WithHttpRequest
logger.Log(logEvent);
```
