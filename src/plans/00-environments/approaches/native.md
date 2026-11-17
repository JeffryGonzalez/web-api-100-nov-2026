# Native (Install Everything Locally)

The "classic" approach: every developer installs the backing services (database, message broker, cache, etc.) directly on their own machine, using installers or a package manager (winget, Chocolatey, Homebrew, apt), and runs them as local services or processes.

Your app's connection strings just point at `localhost`.

## Advantages

- Conceptually simple - no extra layers, no container runtime, no orchestration to learn.
- Best raw performance - services run directly on the metal, no virtualization overhead.
- Great for learning what the services *actually are* - you see the config files, the data directories, the ports.
- Works fine offline.

## Disadvantages

- "Works on my machine" - every developer's setup drifts a little differently over time.
- Onboarding is a slog: a long README of install steps that is out of date the day after it's written.
- Version conflicts: hard to work on two projects that need different versions of PostgreSQL (or whatever) at the same time.
- Cleanup is messy - uninstalling rarely removes everything, and your machine accumulates cruft.
- Your laptop looks nothing like production, so environment-specific bugs show up late.
