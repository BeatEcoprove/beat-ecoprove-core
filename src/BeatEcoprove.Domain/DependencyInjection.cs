using BeatEcoprove.Domain.ProfileAggregator.Factories;

using Microsoft.Extensions.DependencyInjection;

namespace BeatEcoprove.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(
        this IServiceCollection services)
    {
        services.AddScoped<IProfileFactory, ProfileFactory>();
        
        return services;
    }
}