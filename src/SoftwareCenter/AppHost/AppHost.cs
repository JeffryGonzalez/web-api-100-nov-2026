using Aspire.Hosting.Yarp;
using Aspire.Hosting.Yarp.Transforms;
using Scalar.Aspire;

var builder = DistributedApplication.CreateBuilder(args);

var pg = builder.AddPostgres("postgres");

var softwareDb = pg.AddDatabase("software-db");

var scalar = builder.AddScalarApiReference(options =>
{
    options.PreferHttpsEndpoint = true;
    options.AllowSelfSignedCertificates = true;
});

var softwareApi = builder.AddProject<Projects.Software_Api>("software-api")
    .WaitFor(softwareDb)
    .WithReference(softwareDb);

// start my frontend
var frontend = builder.AddViteApp("frontend", "../frontend", "start");

scalar.WithApiReference(softwareApi);

// yarp integration - so like a path in openshift.
builder.AddYarp("gateway")
    .WithHttpsEndpoint(9999)
    .WithConfiguration(yarp =>
    {
        // any requests coming in go to the frontend
        yarp.AddRoute(frontend);
        // unless they start with "/api/", then send it to the software api, but remove the "/api" part.
        yarp.AddRoute("/api/{**catchall}", softwareApi).WithTransformPathRemovePrefix("/api");
    });
builder.Build().Run();
