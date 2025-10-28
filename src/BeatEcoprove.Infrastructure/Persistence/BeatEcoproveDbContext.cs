using BeatEcoprove.Application.Shared.Interfaces.Persistence;
using BeatEcoprove.Domain.ClosetAggregator;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;
using BeatEcoprove.Domain.Shared.Models;
using BeatEcoprove.Infrastructure.Configuration;
using BeatEcoprove.Infrastructure.Extensions;
using BeatEcoprove.Infrastructure.Persistence.Interceptors;
using BeatEcoprove.Infrastructure.Persistence.Shared;

using Microsoft.EntityFrameworkCore;

namespace BeatEcoprove.Infrastructure.Persistence;

public class BeatEcoproveDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    private readonly SoftDeleteInterceptor _softDeleteInterceptor;
    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;

    public BeatEcoproveDbContext(
        SoftDeleteInterceptor softDeleteInterceptor,
        PublishDomainEventsInterceptor publishDomainEventsInterceptor)
    {
        _softDeleteInterceptor = softDeleteInterceptor;
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }

    public DbSet<Profile> Profiles { get; init; } = null!;
    public DbSet<Cloth> Cloths { get; init; } = null!;
    public DbSet<Bucket> Buckets { get; init; } = null!;

    public string GetConnectionString()
    {
        return Env.Postgres.ConnectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_softDeleteInterceptor);
        optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);

        optionsBuilder.UseNpgsql(Env.Postgres.ConnectionString, builder =>
        {
            builder.MigrationsAssembly("BeatEcoprove.Infrastructure");
            builder.UseNetTopologySuite();
        });

        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");
        modelBuilder.HasPostgresExtension("postgis_topology");

        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(BeatEcoproveDbContext).Assembly);

        modelBuilder
            .SetUpGlobalQueryFilters<ISoftDelete>((entity) => entity.DeletedAt == null);

        BeatEcoproveSeeder.Seed(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}