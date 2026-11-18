using Aspire.Hosting.Yarp;
using Aspire.Hosting.Dotnet;
using Aspire.Hosting.Yarp.Transforms;
using Scalar.Aspire;

var builder = DistributedApplication.CreateBuilder(args);

var pg = builder.AddPostgres("postgres")
    .WithDataVolume("pg-data")
    .WithLifetime(ContainerLifetime.Persistent);

var softwareDb = pg.AddDatabase("software-db");

var scalar = builder.AddScalarApiReference(options =>
{
    options.PreferHttpsEndpoint = true;
    options.AllowSelfSignedCertificates = true;
});




var softwareApi = builder.AddDotnetProject("software-api",
                                           "../Software.Api")
    .WaitFor(softwareDb)
    .WithReference(softwareDb);


// start my frontend
var frontend = builder.AddViteApp("frontend", "../frontend", "start");

scalar.WithApiReference(softwareApi);
// C:\Users\Student\class\src\SoftwareCenter\WedsDemo.Api\WedsDemo.Api.csproj

var demo = builder.AddDotnetProject("wedsdemo-api", "../WedsDemo.Api");

// yarp integration - so like a path in openshift.
builder.AddYarp("gateway")
    .WithHttpsEndpoint(9999)
    .WithConfiguration(yarp =>
    {
        // any requests coming in go to the frontend
        yarp.AddRoute(frontend);
        // unless they start with "/api/", then send it to the software api, but remove the "/api" part.
       yarp.AddRoute("/api/{**catchall}", softwareApi).WithTransformPathRemovePrefix("/api");
       yarp.AddRoute("/demo/{**catchall}", demo).WithTransformPathRemovePrefix("/demo");
    });

builder.Build().Run();
