using Microsoft.AspNetCore.Connections;
using TorneoGalactico.Api.Data.Conection;
using TorneoGalactico.Api.Data.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<ISqliteConnection, SqliteConnection>();
builder.Services.AddSingleton<DatabaseInitializer>();

var app = builder.Build();

app.Services.GetRequiredService<DatabaseInitializer>().Initialize();

// Configure the HTTP request pipeline.
app.MapOpenApi();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "Torneo Galáctico API v1");
    options.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
