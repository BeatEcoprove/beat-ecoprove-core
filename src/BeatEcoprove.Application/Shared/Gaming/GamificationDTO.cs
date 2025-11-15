using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;

namespace BeatEcoprove.Application.Shared.Gaming;

public sealed record GamificationDTO(
    Profile profile,
    double NextLevelUp
);