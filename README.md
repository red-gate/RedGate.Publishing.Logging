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

