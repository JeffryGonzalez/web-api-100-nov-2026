# Create the environment

We are taking the [Aspire](./approaches/aspire.md) approach

## Approaches Considered

- [Native (install everything locally)](./approaches/native.md)
- [Shared dev environment](./approaches/shared-dev.md)
- [Docker Compose and containers](./approaches/docker-compose.md)
- [Kubernetes / OpenShift Local](./approaches/k8s.md)
- [Dev containers / cloud development environments](./approaches/dev-containers.md)
- [Ephemeral / on-demand (Testcontainers-style)](./approaches/testcontainers.md)
- [Aspire](./approaches/aspire.md) ⬅️ our pick

## Reasoning

- Ease of use / setup
- Dashboard, including OTEL (logs, traces, metrics)
- Integrations
- We are not using the publish feature, however

## Plan

If you do things in this order, Visual Studio helps you along (each project you add gets wired up to the ones that came before).

1. **Create the solution with the AppHost project first.** Use the "Aspire AppHost" template. The AppHost is the orchestrator - the thing you F5 - so it's the anchor for everything else.
2. **Add a ServiceDefaults project.** This is shared configuration (OTEL, health checks, resilience, service discovery) that every service in the solution will reference. Adding it now means later projects pick it up automatically.
3. **Add the API project, enlisted in Aspire orchestration.** Use the ASP.NET Core Web API template with OpenAPI support enabled, and check "Enlist in Aspire orchestration" so the AppHost knows about it. (Skip container support - Aspire handles that concern.)
4. **Pin stable ports.** Edit each project's `launchSettings.json` so the dashboard and the API run on fixed, HTTPS-only ports. Randomly assigned ports make documentation, bookmarks, and muscle memory useless.
5. **Run it and verify.** `Ctrl+F5`, confirm the dashboard loads and shows your API as a running resource. Don't build on an environment you haven't seen work.
6. **Add an OpenAPI UI (Scalar) to the AppHost.** Register it as a resource and point it at the API's OpenAPI document, so you can exercise endpoints from the dashboard without any extra tooling.
7. **Remove the template scaffolding.** Delete the sample (weather forecast) endpoint - leftover demo code becomes mystery code later.
8. **Prove the pipeline with a "fake" endpoint.** Add your first resource's models and a controller that returns hard-coded data. No database yet - the point is to verify the request/response flow end-to-end (create, get-by-id, list) through the OpenAPI UI before adding real persistence.

The result: one keypress starts the whole environment, with observability and interactive API docs built in - the foundation every later lab builds on.

