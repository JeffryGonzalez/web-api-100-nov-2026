# Carter (Modules over Minimal APIs)

A thin community library that organizes minimal API endpoints into *modules*: classes implementing `ICarterModule` that register their own routes, discovered automatically at startup. The handlers themselves are plain minimal API handlers - Carter only addresses the "where do my endpoints live?" question.

It sits between raw minimal APIs and [FastEndpoints](./fastendpoints.md) on the opinionation scale: a little structure, no new programming model.

## Advantages

- Solves minimal APIs' main organizational weakness (the junk-drawer `Program.cs`) with almost nothing to learn.
- Everything you know about minimal APIs still applies - `TypedResults`, route groups, endpoint filters.
- Very small surface area - easy to adopt, easy to remove.

## Disadvantages

- Still a third-party dependency, for something you can do yourself with a few extension methods and `MapGroup`.
- Assembly scanning to find modules reintroduces a little of the runtime "magic" minimal APIs avoided.
- Niche enough that it's another name on the onboarding list rather than shared vocabulary.
