using BeatEcoprove.Domain.Shared.Broker;

namespace BeatEcoprove.Domain.ProfileAggregator.Events;

public record ProfileCreatedEvent() : IAuthEvent
{
    public Guid ProfileId { get; init; } = Guid.Empty;
}