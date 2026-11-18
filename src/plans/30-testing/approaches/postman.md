# Exercise the API with Postman Collections

Postman is a familiar enterprise starting point: save requests, supply
environment variables, inspect responses, and add scripted assertions.
Collections can also run unattended using a compatible command-line
runner such as Newman or the Postman CLI. "We use Postman" need not mean
"somebody clicks Send before the release."

Requests normally treat the API as a black box. After a POST, follow its
`Location` and check the returned resource. If the API deliberately has
no read operation, an HTTP-only collection cannot directly prove the
stored state; it needs another supported observation mechanism or a
separate gray-box test, not a test-only production endpoint.

Postman does **not** imply system integration testing. A collection
against an API and dependencies started entirely inside a pipeline can
be a system test; the same collection against a shared test environment
is a system integration test in our vocabulary.

## Advantages

- **Very approachable.** Developers, testers, and support staff can inspect and share concrete HTTP examples.
- **Good for exploration.** Trying an unfamiliar API and turning a useful request into a regression check is straightforward.
- **Language-independent.** The client does not need to reference the API's .NET project or its DTOs.
- **Can be automated.** A collection runner can publish pipeline results instead of relying on a manual checklist.

## Disadvantages

- **A second programming surface.** Assertion scripts, collection files, and environment variables need the same review and maintenance as C# tests.
- **No in-process DI access.** Database setup and inspection need separately managed tooling; the HTTP client does not provide them.
- **Environment management is easy to get wrong.** A saved URL can accidentally point at a shared database-backed deployment rather than a disposable pipeline stack.
- **Long request chains become fragile.** Shared variables, execution order, and cleanup can hide dependencies between tests.
- **Product and data policies matter.** Check licensing, account requirements, and workspace synchronization before putting company requests or credentials into a tool.

