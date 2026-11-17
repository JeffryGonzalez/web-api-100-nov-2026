# FastEndpoints (REPR Pattern)

A popular community library built on top of ASP.NET Core that gives you one class per endpoint, following the REPR pattern (Request-Endpoint-Response). You get the organizational structure of controllers with performance on par with minimal APIs.

Think of it as an opinionated answer to "how should I organize my minimal API endpoints?" - the opinions are baked in so the team doesn't have to invent (and argue about) its own.

## Advantages

- Enforced structure - every endpoint looks the same, which pays off on large teams and large APIs.
- Performance comparable to raw minimal APIs - no controller-style per-request reflection or allocation overhead.
- Batteries included - first-class integration for validation (FluentValidation), security, versioning, and testing.
- Good documentation and an active community.

## Disadvantages

- A third-party dependency at the very core of your application - you're betting the architecture on a community project's continued maintenance.
- Its own conventions to learn, on top of ASP.NET Core's - and that knowledge transfers less directly to other jobs.
- Smaller talent pool - fewer developers arrive knowing it.
- For a class, it would teach the library instead of the framework underneath it.
