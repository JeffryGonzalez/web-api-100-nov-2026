# gRPC (with JSON Transcoding)

Instead of designing HTTP endpoints, you define your API as a *service contract* in a `.proto` file; the tooling generates strongly-typed client and server code, and messages travel as binary Protobuf over HTTP/2. JSON transcoding can additionally expose those same services as conventional RESTish HTTP/JSON endpoints.

This is a different paradigm - RPC ("call a method") rather than REST ("manipulate a resource") - and it's common for internal service-to-service communication in microservice systems.

## Advantages

- Serious performance - binary serialization and HTTP/2 multiplexing beat JSON-over-HTTP/1.1 handily.
- Contract-first with generated code - clients and servers can't drift apart; breaking changes show up at compile time.
- Excellent cross-language interop - a .NET service, a Go service, and a Python client all share one contract.
- Streaming (client, server, and bidirectional) is built in.

## Disadvantages

- Not browser-friendly - browsers can't speak native gRPC, so public/web-facing APIs need transcoding or a gateway anyway.
- Harder to explore and debug - you can't just poke at it with a URL; you need tooling that understands Protobuf.
- The contract-first/codegen workflow is a bigger conceptual lift than mapping an HTTP route.
- Valid, but wrong for this class - we're here to learn HTTP API design itself; gRPC abstracts away the very thing we're studying.
