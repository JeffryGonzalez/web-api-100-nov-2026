# Supplement Behavioral Tests with Contract Checks

Contract testing asks whether an API and its consumers agree on the
messages they exchange. Schema-based checks compare responses or an
OpenAPI document with an agreed shape. Consumer-driven tools such as
Pact let consumers publish expectations that the provider verifies.
These are related techniques, not interchangeable products.

OpenAPI compatibility checks and generated or property-based request
testing are increasingly useful additions to API pipelines. They can
find a removed field, an incompatible type, or an unexpected response
for an input nobody handpicked. They supplement
[behavioral API tests](./alba.md); they do not prove that a successful
POST actually saved the right data.

The level follows the execution boundary. An offline schema comparison
is not a system test at all; provider verification may be unit
integration or system testing depending on its internal doubles. It
does not need to call live consumers or a shared environment.

## Advantages

- **Protects independently released consumers.** Detecting a breaking contract change before deployment is cheaper than coordinating an incident.
- **Tests the wire, not just shared types.** Independent expectations can catch mistakes that server and test DTOs accidentally share.
- **Broadens input exploration.** Schema-driven generation can supplement handpicked happy paths and boundary examples.
- **Can stay inside the pipeline.** Contract artifacts and a locally hosted provider avoid a dependency on another team's running service.

## Disadvantages

- **An accurate schema is not accurate behavior.** A 201 response can match the schema while persistence, authorization, or a business rule is wrong.
- **Consumer-driven workflows need ownership.** Publishing, versioning, provider states, and compatibility decisions introduce coordination and sometimes a broker.
- **Generated cases need guardrails.** Reproducible seeds, resource limits, and isolated data matter, especially for destructive operations.
- **Too much ceremony for today's resource.** We can teach concrete wire assertions now without introducing a second contract-management workflow.
- **Not all constraints appear in OpenAPI.** Business semantics and cross-field rules still need explicit examples.
