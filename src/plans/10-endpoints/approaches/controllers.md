# MVC Controllers

The classic ASP.NET way: a class decorated with `[ApiController]`, one method per action, with attribute routing (`[HttpGet("{id}")]`), model binding, and `ActionResult<T>` return types. This lineage goes back through ASP.NET Web API and MVC to the late 2000s.

This is what the vast majority of existing enterprise .NET codebases use, and what most .NET developers think of when they hear "API."

## Advantages

- We have a TON of them - legacy. You *will* read, maintain, and extend controller-based APIs at work regardless of what you'd pick for new code, so you need to know them.
- Well understood - fifteen-plus years of documentation, Stack Overflow answers, team conventions, and library integrations assume controllers.
- Good structure by default - related actions live together in a class, with shared dependencies injected through the constructor.
- A mature filter pipeline (authorization, validation, exception filters) for cross-cutting concerns.

## Disadvantages

- Must use reflection - routes and parameter binding are discovered from attributes at runtime, which costs startup time and fights ahead-of-time (AOT) compilation and trimming.
- Also cause unneeded allocations - EVERY request creates a new instance of the controller class, and those all need to be garbage collected. Minimal APIs skip this entirely.
- `ActionResult<T>` is loosely typed - the compiler won't stop you from returning a shape your signature doesn't advertise, so your OpenAPI docs can lie.
- Ceremony: base class, attributes, conventions - a lot of machinery for "receive a request, return a response."
- Not where Microsoft is investing - new framework capabilities tend to land in minimal APIs first.