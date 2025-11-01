using BeatEcoprove.Domain.Shared.Broker;

namespace BeatEcoprove.Domain.ProfileAggregator.Events;

public record ProfileCreatedEvent(
    Guid AuthId,
    Guid ProfileId,
    string Role
) : IAuthEvent;