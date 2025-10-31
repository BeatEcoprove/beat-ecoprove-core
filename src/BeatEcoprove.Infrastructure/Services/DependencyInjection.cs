using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Infrastructure.Gaming;

using Microsoft.Extensions.DependencyInjection;

namespace BeatEcoprove.Infrastructure.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IValidationFieldService, ValidationService>();
        services.AddScoped<IProfileManager, ProfileManager>();
        services.AddScoped<IClosetService, ClosetService>();
        services.AddScoped<IGamingService, GamingService>();
        services.AddScoped<IStoreService, StoreService>();
        services.AddScoped<IAdvertisementService, AdvertisementService>();
        services.AddScoped<IProfileCreateService, ProfileCreateService>();

        return services;
    }
}