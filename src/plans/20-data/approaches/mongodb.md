# Document Database (MongoDB)

Skip the object-relational mapping problem entirely: store your aggregate (the vendor, with its point of contact, and whatever else belongs to it) as a single JSON-like *document*. No tables, no joins, no impedance mismatch - the shape in the database is the shape in your code.

MongoDB is the name-brand document database, and many enterprise teams (including yours) already run it.

## Advantages

- The mapping problem disappears - save an object, load an object. For aggregate-shaped data (which most API resources are), this is wonderfully direct.
- Schema flexibility - adding a property to your model doesn't require a coordinated migration.
- Scales horizontally by design, and the document model maps naturally onto how APIs already exchange data (JSON in, JSON out).
- Familiar - your organization already uses it.

## Disadvantages

- Consistency and transactions are real considerations - multi-document transactions exist now, but the defaults and habits of the ecosystem lean looser than relational.
- Relational questions ("every vendor referenced by any purchase in Q3") get awkward; reporting teams will grumble.
- Schema flexibility cuts both ways: "flexible" can quietly become "five different shapes of the same document in one collection."
- It's another server with its own operational model, query language, and (SSPL) licensing considerations.
