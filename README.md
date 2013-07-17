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

