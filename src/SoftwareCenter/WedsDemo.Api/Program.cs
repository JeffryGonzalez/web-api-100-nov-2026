var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();



var app = builder.Build();

app.MapDefaultEndpoints();





app.MapGet("/tacos", () => new { toppings="Cheese, Beef, Onion", shell="hard"});

app.Run();

