using Marten;

var builder = WebApplication.CreateBuilder(args);

// add YOUR defaults for all services in terms of config, SRE (retries, circuit breakers, etc.)
// also configures OTEL (tracing, metrics, logs)
builder.AddServiceDefaults();

builder.AddNpgsqlDataSource("software-db");
builder.Services.AddMarten().UseLightweightSessions().UseNpgsqlDataSource();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// above this line is configuring what services are available to the code that will process our requests
// and make responses
var app = builder.Build();
// can no longer change the services collection. The stage is set, so to speak.
// Everything here is what will run when a request is created and/or a response is written.

// add /health endpoints for monitoring.
app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
// it checks if your machine has an environment variable set called ASPNETCORE_ENVIRONMENT=Development
if (app.Environment.IsDevelopment())
{
    // add /openapi/v1.json
    app.MapOpenApi();
}


app.UseAuthorization();

// this won't work unless you have the controller services
// use reflection to read all those attributes and make the route table (phone list)
// if I get GET /vendors - create the vendors controller, call teh get method.
app.MapControllers();

// JIT - jitter, Just in time, method by method compilation at run time. 
app.Run();
