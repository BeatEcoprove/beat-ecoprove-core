using BeatEcoprove.Api;
using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application;
using BeatEcoprove.Infrastructure;
using BeatEcoprove.Infrastructure.Persistence;
using BeatEcoprove.Infrastructure.Persistence.Shared;

using DotNetEnv;

Env.Load($"../../.env");
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddSwagger();

var app = builder
    .Build();

await app.Services.EnsureMigrations<IApplicationDbContext>(
    migrate: true);

app.MapPrometheusScrapingEndpoint();
app.SetupConfiguration();
app.Run();

public abstract partial class Program { }