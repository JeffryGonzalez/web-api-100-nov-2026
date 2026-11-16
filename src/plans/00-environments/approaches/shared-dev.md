# Using a Shared Dev Environment

Roughly, each developer has a minimal developer desktop: An IDE, the cloned repository.

Backing services are in an environment shared by all the developers working on the project:

## Advantages

- legacy, well-known,
- also great if you have stuff like DB2 that you can't run in a container, etc.

## Disadvantages

- makes it tight coupling
- encourages services not to own their own software
- can't work without a connection
- longer onboarding

