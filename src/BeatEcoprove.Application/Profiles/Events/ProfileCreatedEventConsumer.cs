using BeatEcoprove.Domain.ProfileAggregator.Events;

using MassTransit;

namespace BeatEcoprove.Application.Profiles.Events;

public class ProfileCreatedEventConsumer : IConsumer<ProfileCreatedEvent>
{
    public  Task Consume(ConsumeContext<ProfileCreatedEvent> context)
    {
        Console.WriteLine("hey soul sister");
        return Task.CompletedTask;
    }
}