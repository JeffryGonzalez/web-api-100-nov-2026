# GraphQL (Hot Chocolate)

Expose a single endpoint and a *schema*; clients send queries describing exactly the shape of data they want, and the server resolves them. In .NET this usually means the Hot Chocolate library.

Where REST says "the server defines the resources and their shapes," GraphQL says "the client composes what it needs." That inversion is the whole point - and the whole trade-off.

## Advantages

- Clients fetch exactly what they need in one round trip - great for mobile apps and complex UIs aggregating many resources.
- The schema is strongly typed, introspectable, and self-documenting; tooling (playgrounds, codegen) is excellent.
- One evolving schema instead of a proliferation of versioned endpoints and ad-hoc "give me this too" parameters.

## Disadvantages

- You trade REST's problems for a new set: query cost/depth limiting, N+1 resolver problems, and caching (HTTP caching largely stops working - everything is a POST to one endpoint).
- A significant learning curve - schema design and resolvers are their own discipline.
- Overkill when you control both client and server and the data shapes are stable - which is most internal corporate APIs.
- Valid, but wrong for this class - it bypasses the HTTP semantics (verbs, status codes, resources) that we're here to learn.
