namespace BeatEcoprove.Contracts.Profile;

public record PublicProfileResponse
(
    Guid Id,
    string Username,
    int Level,
    int SustainabilityPoints,
    int EcoScore,
    string AvatarUrl
);