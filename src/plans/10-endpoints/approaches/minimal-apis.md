# Minimal APIs

Endpoints are mapped directly onto the app as functions: `app.MapGet("/vendors/{id}", ...)` - no controller class in between. Introduced in .NET 6 and matured since, this is now Microsoft's recommended default for new HTTP APIs. Route groups (`MapGroup`) and endpoint filters give you organization and cross-cutting behavior without the MVC machinery.

The headline feature for correctness is `TypedResults`: a handler can declare it returns `Results<Ok<VendorDetails>, NotFound>`, and the compiler *enforces* that - and the OpenAPI document is generated from it, so docs can't drift from reality.

(If you want more imposed structure on top of this model, that's what community libraries like [FastEndpoints](./fastendpoints.md) and [Carter](./carter.md) provide.)

## Advantages

- Minimal APIs don't use a controller - no per-request controller instance to allocate and garbage collect, so less GC pressure per request.
- Far less reflection - routing is built up explicitly at startup, which means faster startup and a clear path to AOT compilation.
- `TypedResults` - return types are compile-time checked *and* self-documenting to OpenAPI. The docs and the code can't disagree.
- Microsoft's current recommendation - new investment (OpenAPI generation, validation, AOT) lands here first.
- Low ceremony - a route, a handler, done. Great for seeing exactly what an endpoint does.

## Disadvantages

- Less familiar - most corporate .NET developers grew up on controllers, so there's a (small) mental shift.
- Freedom cuts both ways - without discipline (route groups, one file per resource), endpoints can pile up into a junk-drawer `Program.cs`.
- The cross-cutting story is different - endpoint filters instead of MVC's filter pipeline; some older libraries still assume controllers exist.