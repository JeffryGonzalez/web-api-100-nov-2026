var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.VideoGames_Api>("videos-api");

builder.AddProject<Projects.SuperFastStartupApi>("superfaststartupapi");

builder.Build().Run();
