namespace BeatEcoprove.Contracts.Profile;

public record PublicProfileResponse
(
    Guid Id,
    string DisplayName,
    int Level,
    int SustainabilityPoints,
    int EcoScore,
    string AvatarUrl
);