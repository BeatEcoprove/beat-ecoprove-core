using BeatEcoprove.Domain.Shared.Broker;

namespace BeatEcoprove.Domain.ProfileAggregator.Events;

public record UserCreatedEvent(
    Guid AuthId,
    Guid ProfileId,
    string Email,
    string Role
) : IAuthEvent;