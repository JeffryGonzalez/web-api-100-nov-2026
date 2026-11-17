# Raw ADO.NET / Stored Procedures

The original .NET data access story: open a connection, create a command, execute it, walk a data reader, map the columns to your objects by hand. In many enterprises - especially DB2 and older SQL Server shops - the SQL itself lives in *stored procedures* owned by a DBA team, and the application only calls them.

Everything else on this list is, one way or another, an abstraction over this.

## Advantages

- Total control - you see every query, every parameter, every round trip. Nothing is hidden.
- No dependencies, no magic - just the framework and a database driver, which exists for *everything* (including DB2).
- Stored procedures fit organizations where a DBA team owns the database - the interface between app and data is explicit and governed.
- The performance ceiling is as high as your SQL skills.

## Disadvantages

- Boilerplate, everywhere - connections, commands, parameters, readers, and hand-written column-to-property mapping for every single query.
- Easy to do dangerously wrong - string-concatenated SQL is how injection vulnerabilities happen.
- Business logic drifts into stored procedures, splitting your application across two codebases with two deployment processes.
- Hard to test - and the hand-mapping code is exactly the kind of tedious, error-prone work the abstractions were invented to remove.
