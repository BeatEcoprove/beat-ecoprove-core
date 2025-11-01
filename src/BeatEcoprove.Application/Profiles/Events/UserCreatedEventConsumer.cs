using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Domain.ProfileAggregator.Events;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;

using MassTransit;

namespace BeatEcoprove.Application.Profiles.Events;

public class UserCreatedEventConsumer(
    IKeyValueRepository<string> userRegistry
) : IConsumer<UserCreatedEvent>
{
    private readonly TimeSpan _expire = TimeSpan.FromMinutes(15);

    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var message = context.Message;
        var profileId = ProfileId.Create(message.ProfileId);

        var userKey = new UserCreatedKey(profileId);
        await userRegistry.AddAsync(userKey, string.Join(":", message.AuthId.ToString(), message.Role), _expire);
    }
}