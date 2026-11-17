# Marten (Document Database on PostgreSQL)

Marten is a .NET library that turns PostgreSQL into a full document database: your objects are stored as JSON (in Postgres's indexed, queryable `jsonb` columns), and you work with them through a clean, LINQ-queryable API - no mapping configuration, no migrations for shape changes, Marten manages the underlying schema itself.

In other words: the ergonomics of [MongoDB](./mongodb.md) with the operational story of boring, fully-ACID, everybody-supports-it PostgreSQL. (Marten also happens to be one of the best event-sourcing libraries in .NET - same library, if we want to go there later.)

## Advantages

- Document simplicity - `session.Store(vendor)`, `session.LoadAsync<Vendor>(id)`, done. Persistence stays a supporting actor instead of the main plot.
- Real ACID transactions, from a real relational engine - document ergonomics without the document-store consistency caveats.
- No migration ceremony while you're designing - evolve your model freely; Marten handles the schema.
- LINQ queries against your documents, strongly typed.
- One boring database: PostgreSQL is free, battle-tested, and (for us) one line of Aspire integration.
- A built-in growth path to event sourcing with the same library.

## Disadvantages

- PostgreSQL-only - and most of your organizations run DB2, SQL Server, or Mongo. The *library* may not follow you to work; the *concepts* will.
- A smaller community than EF Core or Mongo - fewer Stack Overflow answers, more reading of (admittedly good) docs.
- Document-model trade-offs still apply: heavily relational querying and reporting take extra thought.
- It's a third-party library at the heart of your persistence - the same bet-on-maintenance concern as any community dependency.
