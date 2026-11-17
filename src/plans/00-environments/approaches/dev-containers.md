# Dev Containers / Cloud Development Environments

Define the *entire development environment* - the SDK, tools, extensions, and backing services - as code (a `devcontainer.json`, usually alongside a Dockerfile or compose file). The developer's editor (VS Code, JetBrains, etc.) runs *inside* that containerized environment.

The same definition can run locally against your container runtime, or in the cloud via GitHub Codespaces (or similar), where you need nothing on your machine but a browser or an editor.

## Advantages

- Onboarding in minutes: clone the repo (or open a Codespace) and everything is already there, at the right versions.
- The environment is version-controlled with the code - "works on my machine" becomes "works on *every* machine."
- Each project is fully isolated; switching projects can't break your setup.
- Cloud variants work from any device and don't tax your laptop.

## Disadvantages

- Requires a container runtime locally (or a paid cloud service).
- There's a learning curve to authoring and debugging the devcontainer definition itself.
- Can feel "laggy" or opaque when something goes wrong inside the container (ports, file mounts, Git credentials).
- Cloud environments need a network connection and cost money while running.
