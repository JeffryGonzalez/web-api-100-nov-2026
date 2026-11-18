# Exercise a Running API with HttpClient

Start the API as a real process or container, then use .NET's ordinary
`HttpClient` from xUnit to send requests. This is the familiar
code-first alternative to a Postman collection: setup, assertions, and
cleanup all live in C#.

Unlike the usual in-process
[WebApplicationFactory](./web-factory.md) setup, this approach crosses a
real socket and can exercise the chosen web server. The pipeline can
own the API, PostgreSQL, and any external-service stubs. Pointing these
same tests at a deployed shared test environment changes their role to
system integration tests; `HttpClient` itself does not choose a level.

## Advantages

- **No special API testing DSL.** Request construction, JSON helpers, and assertions are ordinary .NET code.
- **Tests real HTTP transport.** Useful when server hosting, headers, streaming, or another transport-sensitive behavior is the concern.
- **Independent of the implementation language.** A C# test can exercise a .NET API today and another implementation tomorrow.
- **Fits disposable full-stack environments.** Aspire testing, containers, or pipeline scripts can manage the application and its dependencies.

## Disadvantages

- **We own the lifecycle plumbing.** Startup, dynamic addresses, readiness checks, logs, cancellation, and cleanup all need deliberate handling.
- **Usually slower than in-process tests.** Process startup and real network traffic add cost without necessarily adding useful confidence for every endpoint.
- **No direct host service provider.** Gray-box database checks need their own connection and configuration.
- **Assertions are more verbose.** It is easy to accumulate homegrown helpers that recreate a smaller, less documented Alba.
- **A socket is not a production environment.** Local HTTP does not automatically test production certificates, gateways, or deployment settings.