# Ephemeral / On-Demand (Testcontainers-Style)

Rather than keeping an environment "running," the application (or its tests) spins up the backing services it needs *on demand* as throwaway containers, and tears them down when it's done. In .NET this is typically done with the [Testcontainers](https://dotnet.testcontainers.org/) library.

Note: Aspire actually uses a similar idea under the hood - your backing services start when you F5 and go away when you stop. This approach is the DIY, code-first version of that.

## Advantages

- Zero standing infrastructure - nothing to remember to start, stop, or clean up.
- Every run starts from a known, pristine state (great for integration tests).
- The environment requirements live *in the code*, so they can't drift from reality.
- Easy to run in CI with no special setup.

## Disadvantages

- Startup cost on every run - pulling images and waiting for services to be healthy.
- Requires a container runtime.
- Awkward for exploratory work where you *want* data to stick around between runs (you can configure persistence, but it fights the model).
- Mostly a testing story; using it as your whole dev environment takes some plumbing.
