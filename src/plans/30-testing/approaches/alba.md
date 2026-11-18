# Test the API In-Process with Alba

Alba builds on ASP.NET Core's in-process testing infrastructure and gives
us a scenario language for making requests and checking responses. The
test starts the actual application, exercises its HTTP pipeline, and can
also reach its dependency injection container. xUnit still discovers and
runs the tests; Alba is the API testing harness, not a replacement for it.

This is the same family of approach as
[WebApplicationFactory](./web-factory.md), with less request/response
plumbing. We can use real persistence with a disposable PostgreSQL
container, replace selected services, or stub authentication and supply
claims. Those choices determine the **test level**, not the Alba name:
an authentication stub inside the API makes it a unit integration test
under our company definitions.

## Advantages

- **Tests read like HTTP examples.** Status, JSON-path, and header assertions keep the API contract visible without much ceremony.
- **Exercises real framework behavior.** Routing, model binding, validation, serialization, filters, and middleware participate instead of being imagined by a controller unit test.
- **Supports black-box and gray-box tests.** We can follow a returned `Location`, or open a DI scope to arrange and inspect actual database state when the API does not expose it.
- **Makes identity scenarios convenient.** Authentication stubs and per-scenario claims help cover authorization decisions without contacting a corporate identity provider.
- **Fits a pipeline-owned environment.** The API runs in the test process and Testcontainers supplies a throwaway database; no shared deployment is necessary.

## Disadvantages

- **Convenience can hide a boundary change.** Stubbed authentication tests downstream behavior, not the production authentication handler or token validation.
- **It is not a network deployment test.** In-process requests do not prove Kestrel sockets, TLS, proxies, or deployed configuration.
- **The database still costs something.** PostgreSQL containers need a supported container runtime, image access, memory, and startup time on laptops and agents.
- **Shared fixtures need discipline.** A shared database or mutable clock can make tests order-dependent unless we deliberately isolate state.
- **There is another library to learn.** Teams happy with plain `HttpClient` and WebApplicationFactory may not need the extra vocabulary.

