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

scalar.WithApiReference(softwareApi);

builder.Build().Run();
