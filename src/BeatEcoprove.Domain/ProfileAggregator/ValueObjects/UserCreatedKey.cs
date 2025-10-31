using BeatEcoprove.Domain.Shared.Models;

namespace BeatEcoprove.Domain.ProfileAggregator.ValueObjects;

public record UserCreatedKey(ProfileId ProfileId)
    : Key("user_created", ProfileId.Value.ToString());