# Create the HTTP Endpoints

We are starting with the [Controllers](./approaches/controllers.md) approach, and adopting [Minimal APIs](./approaches/minimal-apis.md) for new work as the class goes on.

> **A classroom decision.** ASP.NET Core will happily host controllers and minimal endpoints in the same application, and we're going to take advantage of that so you get hands-on experience with both. On a real project, you would normally **pick one style for new work and be consistent** - mixing them makes a codebase harder to navigate and review. We're mixing them *on purpose*, because comparing them side by side is the lesson.

## Approaches Considered

- [MVC Controllers](./approaches/controllers.md) ⬅️ where we start
- [Minimal APIs](./approaches/minimal-apis.md) ⬅️ where we're headed
- [FastEndpoints](./approaches/fastendpoints.md)
- [Carter](./approaches/carter.md)
- [gRPC (with JSON transcoding)](./approaches/grpc.md)
- [GraphQL](./approaches/graphql.md)

## Reasoning

- Controllers first, because they're what you're most familiar with - and overwhelmingly what you'll encounter in existing codebases at work.
- Minimal APIs for new work, because they're Microsoft's current recommendation and simply the better machine: no per-request controller allocations, far less runtime reflection, and `TypedResults` keep the code and the OpenAPI docs honest.
- The community libraries (FastEndpoints, Carter) are worth knowing about, but we want to learn the framework, not a wrapper around it.
- gRPC and GraphQL solve real problems, but different ones - they abstract away or replace the HTTP semantics this class is about.
- Honest caveat: a mixed-style API is something we'd flag in a code review on a real project. We're accepting that for pedagogy.

## Plan

1. **Model the resource first.** Define the request and response models (create, details, summary/list shapes) before any endpoint code - the API's contract is the design decision; the endpoint style is an implementation detail.
2. **Implement the first resource with a controller, returning fake data.** Hard-coded responses, no database - prove the full request/response flow (create, get-by-id, paginated list) through the OpenAPI UI before adding real behavior.
3. **Verify it from the client's perspective.** Exercise every endpoint through the OpenAPI UI and check the status codes, `Location` headers, and response shapes - the things a consumer of your API actually sees.
4. **Implement the next resource with minimal API endpoints.** Use a route group (`MapGroup`) to keep the resource's endpoints together in their own file, and `TypedResults` so the possible responses are compile-time checked and self-documenting.
5. **Compare the two side by side.** Same HTTP behavior from the outside; different mechanics inside (reflection, per-request allocations, how return types flow into the OpenAPI document). Being able to articulate this difference *is* the discernment we're after.
6. **Default to minimal for the rest of the class.** New endpoints go in minimal style; the controller stays as a living example of the code you'll maintain at work.

The result: one API that demonstrates both endpoint styles, and - more importantly - you can explain when and why you'd choose each.
