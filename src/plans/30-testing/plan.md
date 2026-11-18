# Build Confidence in the API Without Leaving the Pipeline

We are taking the [Alba in-process API testing](./approaches/alba.md)
approach with xUnit and disposable PostgreSQL via Testcontainers,
supplemented by [focused unit tests](./approaches/isolated-unit-tests.md)
where there is a useful rule to isolate.

> **A classroom decision.** We will deliberately compare black-box and
> gray-box assertions, and unit integration and system tests, using a
> small API. Some tests start red because making the promised behavior
> real is the lesson. On a real project, keep the main pipeline green,
> choose each test boundary for the risk it covers, and avoid repeating
> every scenario at every level. Alba is the convenience choice here,
> not a company-wide requirement.

## Approaches Considered

- [Direct unit tests and focused doubles](./approaches/isolated-unit-tests.md) - our level-1 complement
- [WebApplicationFactory and HttpClient](./approaches/web-factory.md) - the mainstream in-process foundation
- [Alba](./approaches/alba.md) ⬅️ our primary API harness
- [HttpClient against a running API](./approaches/dotnet-http-client.md) - useful when real transport matters
- [Postman collections](./approaches/postman.md) - familiar exploration and executable examples
- [Playwright API tests](./approaches/playwright.md) - especially useful alongside an existing UI suite
- [Contract and schema-based testing](./approaches/contract-testing.md) - a useful later supplement, not today's harness

## Reasoning

- The important promises are HTTP promises: status codes, headers, validation, identity, response shapes, and persisted effects. An in-process request tests much more of those promises than a direct controller call.
- Alba makes those promises easy to read while still letting us arrange or inspect data through a DI scope.
- A disposable **real PostgreSQL engine** exercises Marten mapping and query behavior. An in-memory replacement would remove exactly the risk we want to cover.
- Focused doubles remain useful for cheap rule coverage and controlled collaborator outcomes. We do not need to mock every dependency or force every endpoint into a unit test.
- Both of the first two company levels must run from pipeline-owned resources. A green build must not depend on a shared database, a corporate identity service, or somebody's running AppHost.
- Honest caveats: containers add startup and agent requirements, in-process requests do not prove deployment/network behavior, and gray-box assertions couple selected tests to persistence details.

### Use the Company's Level Names

The **SUT is the API**, not the whole distributed enterprise. Classify
the test by what executes inside that boundary and where its
dependencies live, not by its test framework, class name, or trait.

| Company level | Boundary and doubles | Where it runs |
| --- | --- | --- |
| **1: Unit and unit integration** | A unit test isolates a small behavior. A unit integration test composes real pieces but has at least one internal test double, such as a substituted data service or authentication handler. | Developer machine and pipeline; no shared external dependencies. |
| **2: System tests** | The API uses its production implementation path with no internal test doubles. Pipeline-local dependency replacements are allowed: real PostgreSQL provisioned by Testcontainers, or a WireMock server standing in for an external HTTP service. | Developer machine and pipeline; the run owns its dependencies. |
| **3: System integration tests** | Exercise the deployed system and its integrations in a test environment. | Outside this course and outside the self-contained checks described here. |

For this course, a controlled `TimeProvider` is an explicit exception:
it does **not** move a test from system to unit integration. A
PostgreSQL container is real database software in a disposable
environment, not a fake implementation of Marten.

Keep three separate questions on the board:

- **Level:** are there internal doubles, and which environment owns the dependencies?
- **Visibility:** black box means observable HTTP behavior; gray box means we also arrange or inspect internals, such as stored documents. Either can be a system test.
- **Purpose:** validation, authorization, persistence, contract, performance, and so on. These are concerns to cover, not additional company levels.

Replacing an outbound `HttpClient` handler inside the API is an
internal double (unit integration). Pointing the production HTTP client
at a pipeline-local WireMock server preserves that client path and can
be a system test. Similarly, supplying claims through an authentication
stub tests authorization decisions, not real credential validation.

### What the Current Code Gives Us

This is the Day 3 starting point, not a claim that the suite is green.
The existing fixture and class names use "system" more broadly than
the definitions above.

| Existing example | What it establishes or intends to establish | Level / next lesson |
| --- | --- | --- |
| [Authenticated fixture](../../SoftwareCenter/Software.Tests/Vendors/Fixtures.cs) | Starts PostgreSQL, hosts the API, fixes the clock, and installs `AuthenticationStub`. | **Unit integration**, because authentication is replaced inside the SUT. Keep it as a useful fast authorization/behavior fixture, but label it accurately. |
| [Adding a vendor](../../SoftwareCenter/Software.Tests/Vendors/AddingAVendor.cs) | POST returns 201 and selected JSON fields; follows `Location`; inspects a stored document and its timestamp. | A good mix of black-box and gray-box techniques, currently **unit integration** through that fixture. Strengthen identity and ID consistency checks. |
| [Getting vendors](../../SoftwareCenter/Software.Tests/Vendors/GettingVendors.cs) | GET of the collection returns 200. | Currently unit integration; add content, ordering, and pagination assertions. A 200 alone does not establish a correct list. |
| [Manager scenarios](../../SoftwareCenter/Software.Tests/Vendors/OnlyManagers.cs) | An authenticated caller with claims should succeed; one without them should receive 403. | Unit integration authorization examples. Agree which role/policy combination is required; the present pair does not distinguish the effect of each role. |
| [Unauthenticated requests](../../SoftwareCenter/Software.Tests/Vendors/VendorAuthentication.cs) | GET and POST should return 401 without reaching persistence. | Intended system-level rejection checks: this fixture has no authentication stub, but the application must first configure real authentication. The deliberately unusable DB connection only fits paths that must not touch the DB. |
| [Invalid POST](../../SoftwareCenter/Software.Tests/Vendors/PostsAreValidated.cs) | A body missing the expected members returns 400. | Currently unit integration. Add authenticated, authorized cases that isolate individual validation rules and inspect validation details. |
| [Attribute inspection](../../SoftwareCenter/Software.Tests/Vendors/Validations.cs) | Checks selected validation attributes on the create model. | Unit-level metadata checks, not evidence that HTTP validation executes. Supplement with behavior tests; do not make attribute layout the primary contract. |

In the current [application startup](../../SoftwareCenter/Software.Api/Program.cs)
and [controller](../../SoftwareCenter/Software.Api/Vendors/VendorsController.cs),
the authentication registration and endpoint access rules needed by the
401/403 examples are not yet present. The create action also depends on
`TimeProvider`; its production registration needs to exist independently
of the test clock override. These are teaching work to complete, not
reasons to weaken the assertions.

There is a useful substitution seam already:
[the data interface](../../SoftwareCenter/Software.Api/Vendors/IHandleVendorData.cs)
is used by the list endpoint and implemented by
[the PostgreSQL adapter](../../SoftwareCenter/Software.Api/Vendors/PostgresVendorData.cs).
NSubstitute is installed, but the current examples do not yet replace
that service. POST and get-by-ID use Marten directly; replacing the list
service will not isolate those other operations.

### Coverage to Build, Not Just More Test Methods

| Concern | Focused examples / evidence |
| --- | --- |
| Create contract | 201, usable `Location`, nonempty ID, expected response fields, and the same ID and values when retrieved. |
| Persistence and audit data | A fresh scoped session can load the created document; stored values, controlled creation time, and authenticated creator are correct. Internal audit fields need not be added to public response models to test them. |
| Retrieval | A known document maps to the expected details; a valid but absent ID returns 404. Distinguish that from a route that does not match a GUID. |
| Lists | Empty list, default and explicit page sizes, expected ordered items, partial final page, no overlap between pages, and correct page metadata. Use distinct sortable names so expected order is unambiguous. |
| Invalid input | Missing members, null/blank values where prohibited, name length boundaries (currently 5 and 100), invalid URL, and malformed JSON. Agree null/nested-field rules and invalid paging behavior before writing expectations the API never promised. |
| Identity and access | Anonymous 401; authenticated but insufficiently privileged 403; required claims succeed. Use an otherwise-valid body and appropriate identity so authentication, authorization, and validation do not mask one another. |
| Rejected writes | For selected 400/401/403 scenarios, verify no document was created. A status code alone does not prove absence of side effects. |
| Isolated orchestration | Substitute the list-data service to control its output and check HTTP mapping/page metadata. This does not prove the real query's ordering or pagination; keep those checks against PostgreSQL. |
| Real authentication path | Once the production scheme is chosen, exercise it without `AuthenticationStub`, using pipeline-local credentials/key material and any required local identity dependency. Check accepted and rejected credentials; keep issuer, audience, signature, and lifetime validation enabled where applicable. |

## Plan

1. **Agree on the test boundary and observable promises.** Write down the company levels, the clock exception, and the expected access and input rules before making red tests green. Treat authentication stubs as internal doubles even if the database is real.
2. **Make the test environment own every dependency it needs.** Start a disposable database, inject its connection string before the host starts, and wait for readiness; use local stand-ins for any external services. Verify there is no fallback to a developer or shared environment, including authentication discovery and optional telemetry export.
3. **Separate fixtures by what they replace.** Keep an explicitly named unit integration fixture for authentication/service substitutes and a system fixture using production API services and authentication configuration. A no-database rejection fixture is an optimization only after verifying that startup and rejected requests require no database access.
4. **Establish state isolation and reliable cleanup before growing the suite.** Share expensive hosts/containers only where safe; use unique records for independent tests and a reset or separate database for exact-count/list tests. Keep resets from racing with other tests, restore mutable clocks/claims, dispose scoped sessions, then stop the host before disposing its database; also clean up after failed initialization.
5. **Prove one successful write through HTTP and persistence.** Assert status, response fields, and `Location`, follow the location, and compare the created and retrieved identity and values. Use a fresh database scope to check persistence and clock-controlled audit data, then run that test alone before adding more scenarios.
6. **Use focused unit integration tests to expose missing request behavior.** Add isolated validation and access scenarios with explicit identities, claims, and otherwise-valid requests; distinguish 400, 401, and 403. Configure the production application to enforce the agreed rules rather than making the fixture enforce them for it, and verify selected rejections leave no stored data.
7. **Demonstrate the value and limits of a service substitute.** Replace one existing collaborator to exercise a controlled result through the real request pipeline, with no unnecessary database work. Compare that passing unit integration test with a real-database test that would still catch a broken query.
8. **Exercise real reads, missing resources, and pagination.** Seed deterministic data through a scoped persistence session, then assert complete response behavior rather than only 200s or item counts. Verify page contents, ordering, metadata, and boundary cases before relying on the collection endpoint.
9. **Prove the system path without internal doubles.** Run representative create/read/list and identity rejection cases with the production authentication handler and persistence implementation, using run-owned identity inputs and dependencies. Verify the fixture does not install an authentication stub or replace application services; if the real authentication path is not ready yet, keep these as planned work rather than relabeling the stubbed tests.
10. **Make both pipeline levels repeatable and diagnosable.** Apply consistent level traits to all relevant tests and run both levels from a clean checkout on a container-capable agent with normal restores/image pulls available but no shared application dependencies. Repeat the run to expose leaked state, verify a deliberate regression fails the relevant test, and retain test results and useful host/container logs; missing infrastructure must fail clearly, not silently skip coverage.

The result: a repeatable, pipeline-contained mix of focused level-1 tests and honest level-2 API tests, with clear evidence of what each proves and what remains for system integration testing elsewhere.
