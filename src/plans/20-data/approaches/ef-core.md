# Entity Framework Core (Full ORM)

Microsoft's flagship object-relational mapper and the mainstream .NET answer to persistence: you define entity classes and a `DbContext`, query with LINQ, and EF translates to SQL, tracks changes to loaded objects, and writes updates back. Migrations generate and version your schema from your C# model.

If an enterprise .NET team uses an ORM, it's almost certainly this one.

## Advantages

- The default answer - enormous documentation, community knowledge, and team familiarity; every .NET developer will encounter it.
- Strongly-typed LINQ queries - the compiler catches a whole class of query mistakes, and refactoring tools work on your queries.
- Migrations manage schema evolution as versioned, reviewable code.
- A provider model that covers SQL Server, PostgreSQL, SQLite, and (with IBM's provider) even DB2.

## Disadvantages

- A leaky abstraction - eventually you must understand *both* EF and the SQL it generates. Change tracking, lazy loading, and N+1 queries are classic footgun territory.
- Heavy machinery - DbContext lifetime, entity configuration, and migrations are a lot of ceremony before your first row is saved.
- Easy to let the database model *become* your domain model, coupling your API's shapes to your tables.
- The learning curve is real, and it's a curve about EF, not about your problem.
