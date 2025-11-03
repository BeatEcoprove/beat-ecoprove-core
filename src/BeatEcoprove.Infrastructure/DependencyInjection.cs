using BeatEcoprove.Domain;
using BeatEcoprove.Infrastructure.Authentication;
using BeatEcoprove.Infrastructure.Broker;
using BeatEcoprove.Infrastructure.EmailSender;
using BeatEcoprove.Infrastructure.FileStorage;
using BeatEcoprove.Infrastructure.Persistence;
using BeatEcoprove.Infrastructure.Providers;
using BeatEcoprove.Infrastructure.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeatEcoprove.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.SetUpRedis();
        services.AddEmailSender();
        services.AddFileStorageConfiguration();
        services.AddProviders();
        services.AddServices();
        services.AddPersistenceLayer();

        services.AddDomain();

        services.AddJwks();
        services.AddBroker();

        return services;
    }
}