# Docker Compose and Containers

Developers have a container runtime installed on their machines (Podman, Docker Desktop, Rancher Desktop, whatever), and use a tool like Docker Compose to start a local developer environment.

The backing services (database, cache, message broker, etc.) are described in a `compose.yaml` file checked into the repo. One command (`docker compose up`) starts them all; your API runs on the host as usual and connects to them on mapped `localhost` ports.

This has been the de facto standard for local dev environments for the last decade or so.

## Advantages

- Nimble, relatively easy - `up` to start, `down` to throw it all away and start fresh.
- Can have multiple versions - run PostgreSQL 15 for one project and 17 for another without them fighting (more on this later).
- The compose file is in source control, so the environment is documented *and* reproducible.
- Nothing is permanently installed on your machine - delete the containers and it's like it never happened.
- Huge ecosystem: practically every service you'd want has an official image and a copy-pasteable example.

## Disadvantages

- A bit challenging to learn and set up - images, ports, volumes, networks, health checks - it's a whole vocabulary.
- Can be a bit flaky - and the compose file is a bunch of stuff you are writing that doesn't go to production.
    - Flaky - containers can be restarted easily, but figuring out *why* a service didn't come up is a pain.
- No built-in observability story - if you want dashboards, telemetry, etc., you're adding and wiring up those services yourself.

