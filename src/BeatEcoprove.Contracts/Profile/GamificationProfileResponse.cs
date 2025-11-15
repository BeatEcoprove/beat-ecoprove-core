namespace BeatEcoprove.Contracts.Profile;

public record GamificationProfileResponse(
    Guid Id,
    string Username,
    int SustainabilityPoints,
    int EcoScore,
    int EcoCoins,
    string AvatarUrl,
    int Level,
    double XP,
    float LevelPercentage,
    double NextLevelUp
);