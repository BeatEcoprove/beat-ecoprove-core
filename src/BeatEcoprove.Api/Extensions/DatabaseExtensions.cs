using BeatEcoprove.Infrastructure.Persistence.Shared;

namespace BeatEcoprove.Api.Extensions;

public static class DatabaseExtensions
{
    public static async Task EnsureMigrations<TContext>(
        this IServiceProvider provider,
        bool migrate = false,
        CancellationToken cancellationToken = default) where TContext : IApplicationDbContext
    {
        using var scope = provider.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<TContext>>();

        try
        {
            var dbContext = services.GetRequiredService<TContext>();

            if (migrate)
            {
                logger.LogInformation("Migrating database schema");
                await dbContext.MigrateAsync(cancellationToken);
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while migrating database schema");
            throw;
        }
    }
}