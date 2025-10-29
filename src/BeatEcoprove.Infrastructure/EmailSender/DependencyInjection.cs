using BeatEcoprove.Application.Shared.Interfaces.Providers;

using Microsoft.Extensions.DependencyInjection;

namespace BeatEcoprove.Infrastructure.EmailSender;

public static class DependencyInjection
{
    public static IServiceCollection AddEmailSender(
        this IServiceCollection services)
    {
        services.AddScoped<IMailSender, MailSender>();

        return services;
    }
}