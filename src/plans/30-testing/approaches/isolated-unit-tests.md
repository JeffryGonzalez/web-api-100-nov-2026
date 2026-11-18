# Test Classes Directly with Focused Doubles

Call a function, validator, service, or controller directly from xUnit
and assert its result. For a class with collaborators, supply a small
handwritten fake or a substitute from a library such as NSubstitute.
This is a longstanding .NET practice and remains a very good fit for
business rules with lots of input combinations.

A controller method called directly is **not** an HTTP request.
ASP.NET Core does not perform routing, binding, model validation,
authentication, authorization, or response serialization for that
call. A directly returned `OkObjectResult` is not proof that the HTTP
endpoint returns the promised JSON.

Direct tests of one unit belong at level 1. Tests composing several
real pieces with at least one internal double are unit integration
tests at that same level. We can also make unit integration tests
through [Alba](./alba.md) by replacing a service in the real HTTP
pipeline; "uses HTTP" does not automatically mean "system test."

## Advantages

- **Fast and precise feedback.** Small tests isolate a rule and usually explain a failure without a host or database log.
- **Cheap boundary coverage.** Many combinations of lengths, dates, and business decisions can run without repeating expensive setup.
- **Deterministic failure scenarios.** A substitute can make a collaborator return a particular result or throw a specific exception on demand.
- **No container prerequisite for pure units.** Useful on constrained laptops and for very short feedback loops.

## Disadvantages

- **Framework behavior is outside the test.** Direct action calls do not prove automatic 400s, 401s, 403s, or JSON contracts.
- **A fake query engine is not PostgreSQL.** Mocking Marten or replacing the database with an in-memory collection cannot establish query translation, ordering, or persistence correctness.
- **Interaction tests can cement implementation details.** Verifying every call makes harmless refactoring look like broken behavior.
- **Doubles can agree with our mistakes.** A substitute returning exactly what the author expects is not independent evidence that the real collaborator works that way.
- **Thin endpoints may offer little to isolate.** Do not invent layers or interfaces just to make a unit-test count go up.
