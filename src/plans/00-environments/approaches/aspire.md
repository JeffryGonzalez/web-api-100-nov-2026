# Use Aspire

Use Microsoft's [Aspire](https://learn.microsoft.com/en-us/dotnet/aspire/) for creating a development environment.

You describe your app and its backing services (databases, caches, brokers, etc.) in C# in an "AppHost" project. When you hit F5, Aspire starts everything - pulling container images for the backing services as needed - wires up connection strings and service discovery automatically, and gives you a dashboard to watch it all.

In other words: it's orchestration for your *inner loop*, written in the language you already know, instead of YAML.

## Advantages

- Super easy - one F5 starts the whole system; no separate "start the environment" step to forget.
- Allows access to OTEL data - logs, traces, and metrics are collected automatically, which is great for teaching observability from day one.
- Has a groovy dashboard - see every resource, its console output, its endpoints, and its telemetry in one place.
- Don't need to learn container details - Aspire's *integrations* (PostgreSQL, Redis, RabbitMQ, etc.) hide the image names, ports, and volumes behind a couple lines of C#.
- Can use it to "publish" your app* - the same app model can generate deployment assets.

## Disadvantages

- A bit to learn - new project types (AppHost, ServiceDefaults) and new concepts - but still easier than the other approaches here.
- Publish is still a bit weak - unless you are publishing to Azure. ;)
- It's Microsoft/.NET-centric; the skills transfer less directly to polyglot teams than, say, Docker Compose.
- You still need a container runtime installed for the backing services.

