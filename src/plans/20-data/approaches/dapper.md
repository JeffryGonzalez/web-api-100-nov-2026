# Dapper (Micro-ORM)

A deliberately small library (built at Stack Overflow) that does one thing: you write the SQL, and Dapper maps the results to your objects and your parameters into the query - removing exactly the boilerplate from the [raw ADO.NET](./ado-net.md) approach while hiding nothing else.

This is the "we know SQL, stop helping" option, and it's very popular in shops with existing databases and strong SQL skills.

## Advantages

- SQL-first - the query you wrote is the query that runs. Easy to reason about, easy to hand to a DBA, easy to tune.
- Very fast, with near-zero overhead - and a learning curve measured in minutes if you already know SQL.
- Works beautifully against *existing* databases and stored procedures - a great fit for DB2/SQL Server enterprises.
- Parameterization is the natural path, so the injection footgun from raw ADO.NET mostly disappears.

## Disadvantages

- You write *all* the SQL - every insert, update, and query, by hand, forever.
- No schema management - migrations and versioning are your problem (teams typically pair it with a migration tool).
- Mapping complex object graphs (parent + children) is manual and gets tedious.
- More code per feature than an ORM; the simplicity is paid for in volume.
