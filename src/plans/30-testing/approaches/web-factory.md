# Test the API In-Process with WebApplicationFactory

`WebApplicationFactory<Program>` is Microsoft's mainstream ASP.NET Core
integration-testing harness. It boots the application with an in-memory
test server and gives the test an `HttpClient`. Tests send requests and
assert the status, headers, and body with their usual test framework.

The factory can override configuration and services, and tests can
access the host's service provider. Like [Alba](./alba.md), it supports
both unit integration tests with internal substitutes and system tests
with production services and pipeline-local dependencies. Microsoft's
term "integration testing" is broader than our company's level names.

## Advantages

- **Standard ASP.NET Core tooling.** Documentation, examples, and existing team experience are easy to find.
- **Real request processing.** Tests cover middleware, routing, binding, validation, and serialization rather than calling an action as an ordinary method.
- **Few additional concepts.** `HttpClient`, DI scopes, and xUnit are useful outside this particular harness.
- **Flexible setup.** The same foundation supports a substituted collaborator or a real PostgreSQL database in Testcontainers.
- **Gray-box access when useful.** Tests can seed or inspect persistence without adding test-only API endpoints.

## Disadvantages

- **More plumbing than Alba.** Sending JSON, reading bodies, and asserting common HTTP details can distract from the behavior being taught.
- **Custom factories can grow into frameworks.** Large inheritance trees and service overrides can make it difficult to tell what is actually real.
- **It still bypasses the network.** A passing in-process test says nothing about TLS, reverse proxies, or production hosting.
- **Infrastructure and isolation remain our problem.** A factory does not automatically provision a database or keep parallel tests from interfering.

See Microsoft's [ASP.NET Core integration testing guide](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests).