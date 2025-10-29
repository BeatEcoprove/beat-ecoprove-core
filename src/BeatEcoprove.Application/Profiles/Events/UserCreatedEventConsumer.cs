using BeatEcoprove.Domain.ProfileAggregator.Events;

using MassTransit;

namespace BeatEcoprove.Application.Profiles.Events;

public class UserCreatedEventConsumer : IConsumer<UserCreatedEvent>
{
    public Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        Console.WriteLine($"Public Id: {context.Message.PublicId}");
        Console.WriteLine($"Email: {context.Message.Email}");
        Console.WriteLine($"Role: {context.Message.Role}");
        
        return Task.CompletedTask;
    }
}