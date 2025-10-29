using BeatEcoprove.Application.Profiles.Events;
using BeatEcoprove.Domain.ProfileAggregator.Events;
using BeatEcoprove.Domain.Shared.Broker;
using BeatEcoprove.Infrastructure.Broker.Serializers;
using BeatEcoprove.Infrastructure.Configuration;

using MassTransit;
using MassTransit.KafkaIntegration.Serializers;

namespace BeatEcoprove.Infrastructure.Broker;

using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    private static void AddConsumers()
    {
        
    }
    
    public static IServiceCollection AddBroker(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));
    
            x.AddRider(rider =>
            {
                rider.AddConsumers(typeof(BeatEcoprove.Application.DependencyInjection).Assembly);

                var authTopic = new TopicBuilder<IAuthEvent>("auth_events", "core_auth_consumers")
                    .WithConsumer<ProfileCreatedEvent, ProfileCreatedEventConsumer>()
                    .WithConsumer<UserCreatedEvent, UserCreatedEventConsumer>()
                    .WithProducer(rider);
                
                rider.UsingKafka((context, kafka) =>
                {
                    kafka.Host($"{Env.Host}:{Env.Port}");
                    
                    authTopic.ApplyConsumers(context, kafka);
                });
            });
        });

        return services;
    }
}