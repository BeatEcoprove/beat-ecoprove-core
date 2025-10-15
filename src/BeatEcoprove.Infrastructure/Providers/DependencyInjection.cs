using BeatEcoprove.Application;
using BeatEcoprove.Application.Shared.Interfaces.Providers;
using BeatEcoprove.Infrastructure.Authentication;

using Microsoft.Extensions.DependencyInjection;

namespace BeatEcoprove.Infrastructure.Providers;

public static class DependencyInjection
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddScoped<IPasswordProvider, PasswordProvider>();
        services.AddScoped<IPasswordGenerator, PasswordGenerator>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }
}