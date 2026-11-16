// top level statements - whatever is in your Program.cs will be added to the static voic Main(string[] args) when you compile.
var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// above this line is configuring services
var app = builder.Build();
// after this line is configuring middleware
app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// we are exposing both http and https, but we will only talk to you on a "secure channel" (https)
//app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run(); // this is a "blocking" loop - it just spins here *forever* listening for HTTP request
