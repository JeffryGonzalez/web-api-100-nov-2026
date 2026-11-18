# Exercise the API with Playwright

Playwright is best known for browser automation, but its API request
context can send HTTP requests without opening a browser. Teams can
write API tests in C# or TypeScript and use the same tool family for
API setup and browser workflows.

API requests are black-box observations by default. That is a choice of
test boundary, not a ban on database access: a Playwright test suite can
use separate database libraries or fixtures, but Playwright does not
give it the API host's DI container. Whether the test is a system test
or a system integration test depends on the environment and doubles,
not on the client library.

## Advantages

- **One tool family across API and UI workflows.** Particularly useful when a team already owns a browser suite.
- **No browser required for API-only tests.** Direct requests avoid paying the browser startup and rendering cost.
- **An independent consumer's perspective.** Sending ordinary HTTP and asserting JSON can expose mistakes hidden by sharing server DTO types.
- **Useful test-runner support.** The TypeScript runner offers fixtures, parallel execution, and reporting; C# integrates with familiar .NET test runners.

## Disadvantages

- **More setup for an API-only .NET class.** A running API and its lifecycle still have to be arranged; TypeScript also adds a second toolchain.
- **No built-in view inside the API.** Seeding or inspecting persistence requires additional setup rather than an existing host service scope.
- **Types do not validate the wire.** TypeScript annotations and C# DTOs cannot prove that received JSON matches a contract at runtime; assertions still matter.
- **Network tests have more moving parts.** Ports, readiness, process cleanup, and test-data isolation must be managed explicitly.
- **Browser dependencies are unnecessary here.** Install them only if browser tests are actually part of the task.

