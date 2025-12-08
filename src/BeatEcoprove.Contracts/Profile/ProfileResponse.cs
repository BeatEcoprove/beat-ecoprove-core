namespace BeatEcoprove.Contracts.Profile;

public record ProfileResponse
(
    Guid Id,
    string DisplayName,
    int Level,
    double XP,
    float LevelPercentage,
    int SustainabilityPoints,
    int EcoScore,
    int EcoCoins,
    string AvatarUrl,
    string PhoneNumber,
    string PhoneCountry
);