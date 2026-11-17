# Persist the Data

We are taking the [Marten](./approaches/marten.md) approach

> **A classroom decision.** Your organizations mostly run DB2, SQL Server
> (with EF Core or Dapper), and MongoDB - and on a real project, the
> existing infrastructure and the ops team's expertise would weigh
> *heavily*, probably decisively. We're choosing Marten because it is the
> **least distracting**: persistence stays a supporting actor so the API
> design stays the main plot. The library may not follow you back to
> work; the concepts - entities vs. wire models, transactions, testing
> against a real database - absolutely will.

## Approaches Considered

- [Raw ADO.NET / stored procedures](./approaches/ado-net.md)
- [Entity Framework Core](./approaches/ef-core.md)
- [Dapper](./approaches/dapper.md)
- [Document database (MongoDB)](./approaches/mongodb.md)
- [Marten (documents on PostgreSQL)](./approaches/marten.md) ⬅️ our pick

## Reasoning

- Document-style persistence has the least ceremony: store and load whole resources, no mapping configuration, no migration ritual before the first row - we stay focused on HTTP and API design.
- Marten gives document ergonomics *with* full ACID transactions, because underneath it's just PostgreSQL - a boring, free, battle-tested database that is one line of Aspire integration away.
- Our API resources are aggregate-shaped (a vendor and its point of contact travel together), which is exactly what documents are good at.
- It also opens a door to event sourcing later, with no new infrastructure.
- Honest caveats: it's PostgreSQL-only (which your shops barely use), the community is smaller than EF Core's, and heavily relational reporting would take extra thought. EF Core and Dapper remain what you're most likely to use at work - read those write-ups.

## Plan

1. **Add the database to the environment, not to your machine.** Add the database via an AppHost integration so it appears as a resource on the dashboard. This is the payoff of our [environment decision](../00-environments/plan.md) - backing services belong to the environment, and nobody installs anything.
2. **Wire the API to the database.** Add the persistence library to the API project and take the connection from the environment (Aspire injects it) - never hard-code a connection string.
3. **Keep entities separate from wire models.** The entity is what you *store*; the models are what you *promise* over HTTP. Map between them deliberately, so a storage change can never silently change your API contract.
4. **Replace the fake endpoints one operation at a time.** Start with create (write it, then read it back), then get-by-id, then the paginated list. Verify each through the OpenAPI UI before moving to the next - and confirm data survives a restart, which the fake version never could.
5. **Watch it in the telemetry.** Open a request's trace on the dashboard and find the database calls inside it. Seeing the real cost and sequence of your queries is how performance intuition gets built.
6. **Prove it with integration tests against a real database.** Spin up a throwaway database per test run ([Testcontainers-style](../00-environments/approaches/testcontainers.md)) and exercise the endpoint-to-database path. Don't mock the database - the mapping and the queries are exactly the code that needs testing.

The result: the API's data survives a restart, every endpoint is backed by real persistence you can observe in the telemetry, and a test suite proves the whole path - while the persistence code stays small enough to ignore.
