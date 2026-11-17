var builder = DistributedApplication.CreateBuilder(args);
// backing services
var pg = builder.AddPostgres("postgres-server")
    .WithLifetime(ContainerLifetime.Persistent);

var videosDatabase = pg.AddDatabase("videos-db");
// dev affordances


// apis
builder.AddProject<Projects.VideoGames_Api>("videos-api")
    .WithReference(videosDatabase)
    .WaitFor(videosDatabase)
    .WithChildRelationship(videosDatabase);


builder.AddProject<Projects.SuperFastStartupApi>("superfaststartupapi");

builder.Build().Run();
