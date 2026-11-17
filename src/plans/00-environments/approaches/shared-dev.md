# Using a Shared Dev Environment

Roughly, each developer has a minimal developer desktop: an IDE and the cloned repository.

Backing services (the database, message broker, other teams' APIs, etc.) live in a centralized "dev" or "integration" environment, maintained by an ops/DBA team and shared by *all* the developers working on the project. Your local app is configured to point at those shared servers.

This was the default model in most enterprises for decades, and it's still common anywhere there's hardware or licensing that can't live on a laptop.

## Advantages

- Legacy, well-known - ops teams understand it, and it matches how many organizations are already structured.
- Also great if you have stuff like DB2 on a mainframe, or expensive licensed software, that you can't run in a container or install locally.
- Minimal demands on the developer's machine - an IDE and a network connection is all you need.
- One environment to patch, back up, and keep consistent.

## Disadvantages

- Makes for tight coupling - one developer's schema change or bad data breaks *everyone's* day.
- Encourages services not to own their own software - the team becomes dependent on another group to provision, configure, and change things.
- Can't work without a connection - no network, no development (plane, outage, VPN trouble).
- Longer onboarding - getting accounts, permissions, and firewall rules can take days or weeks.
- No isolation for experiments: you can't safely wipe the database or try a risky migration when ten other people are using it.

