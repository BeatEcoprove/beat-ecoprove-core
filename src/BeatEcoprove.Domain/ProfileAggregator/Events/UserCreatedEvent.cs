using BeatEcoprove.Domain.Shared.Broker;

namespace BeatEcoprove.Domain.ProfileAggregator.Events;

public record UserCreatedEvent(
    string PublicId, 
    string Email, 
    string Role
) : IAuthEvent;