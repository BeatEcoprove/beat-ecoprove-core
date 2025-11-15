using System.Security.Claims;

namespace BeatEcoprove.Application.Shared.Helpers;

public struct UserClaims
{
    public const string AccountId = "sub";
    public const string Type = "type";
    public const string Role = ClaimTypes.Role;
    public const string StoreId = "store_id";
    public const string ProfileId = "profile_id";
    public const string ProfileIds = "profile_ids";
    public const string Email = "email";
    public const string UserName = "given_name";
    public const string AvatarUrl = "avatar_url";
    public const string Level = "level";
    public const string LevelPercentage = "level_percentage";
    public const string SustainablePoints = "sustainable_points";
    public const string TokenType = "token_type";
    public static readonly string EcoScore = "eco_score";
    public static readonly string EcoCoins = "eco_coins";
    public static readonly string CurrentXp = "current_xp";
    public static readonly string NextLevelXp = "next_level_xp";
}