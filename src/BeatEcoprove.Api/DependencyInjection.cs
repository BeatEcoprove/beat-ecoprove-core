using System.Text.Json;

using Asp.Versioning;

using BeatEcoprove.Api.Mappers;
using BeatEcoprove.Api.Middlewares;

using Carter;

using OpenTelemetry.Metrics;

namespace BeatEcoprove.Api;

public static class DependencyInjection
{
    private static IServiceCollection AddTelemetry(this IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics.AddRuntimeInstrumentation()
                    .AddProcessInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();

                metrics.AddEventCountersInstrumentation(events =>
                {
                    events.AddEventSources("Microsoft-AspNetCore-Hosting");
                    events.AddEventSources("Microsoft-AspNetCore-Server-Kestrel");
                    events.AddEventSources("System-Net-Http");
                });

                metrics.AddPrometheusExporter();
            });

        return services;
    }

    private static IServiceCollection AddApiVersion(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = ApiVersions.Current;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    private static IServiceCollection AddMiddlewares(this IServiceCollection services)
    {
        services.AddTransient<AuthorizationMiddleware>();
        services.AddTransient<PictureFormatterMiddleware>();

        return services;
    }

    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddTelemetry();
        services.AddApiVersion();
        services.AddMiddlewares();
        services.AddCarter();
        services.AddControllers();
        services.AddMappings();

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        });

        services.AddSwagger();
        services.AddCors();

        return services;
    }
}