using TorneoGalactico.Api.Data.Conection;
using TorneoGalactico.Api.Data.Database;
using TorneoGalactico.Api.Middleware;
using TorneoGalactico.Api.Services.BattleServices;
using TorneoGalactico.Api.Services.RankingServices;
using TorneoGalactico.Api.Services.SpeciesServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<ISqliteConnection, SqliteConnection>();
builder.Services.AddSingleton<DatabaseInitializer>();

builder.Services.AddScoped<ISpecieService, SpecieService>();
builder.Services.AddScoped<IBattleServices, BattleService>();
builder.Services.AddScoped<IRankingService, RankingService>();

var app = builder.Build();

app.Services.GetRequiredService<DatabaseInitializer>().Initialize();

app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
app.MapOpenApi();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "Torneo Galáctico API v1");
    options.RoutePrefix = "swagger";
});

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
