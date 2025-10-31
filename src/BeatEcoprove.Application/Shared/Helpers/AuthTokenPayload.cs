using System.Globalization;

using BeatEcoprove.Application.Shared.Interfaces.Helpers;
using BeatEcoprove.Domain.ProfileAggregator.Enumerators;

namespace BeatEcoprove.Application.Shared.Helpers;

public class AuthTokenPayload(
    Guid accountId,
    Guid profileId,
    string email,
    string userName,
    string avatarUrl,
    int level,
    double levelPercentage,
    int sustainablePoints,
    int ecoScore,
    int ecoCoins,
    double currentXp,
    double nextLevelXp,
    UserType userType,
    Tokens tokenType,
    string role = "",
    string storeId = "")
    : TokenPayload(tokenType)
{
    public string AccountId => accountId.ToString();
    public string ProfileId => profileId.ToString();
    public string Email { get; } = email;
    public string UserName { get; } = userName;
    public string AvatarUrl { get; } = avatarUrl;
    public UserType UserType { get; } = userType;
    public string Level => level.ToString();
    public string LevelPercentage => levelPercentage.ToString(CultureInfo.CurrentCulture);
    public string SustainablePoints => sustainablePoints.ToString();
    public string EcoScore => ecoScore.ToString();
    public string EcoCoins => ecoCoins.ToString();
    public string CurrentXp => currentXp.ToString(CultureInfo.InvariantCulture);
    public string NextLevelXp => nextLevelXp.ToString(CultureInfo.InvariantCulture);

    public override IDictionary<string, string> GetPayload()
    {
        var userType = UserType.GetUserType();
        
        var claims = new Dictionary<string, string>
        {
            { UserClaims.AccountId, AccountId },
            { UserClaims.ProfileId, ProfileId },
            { UserClaims.Email, Email },
            { UserClaims.UserName, UserName },
            { UserClaims.AvatarUrl, AvatarUrl },
            { UserClaims.Level, Level },
            { UserClaims.LevelPercentage, LevelPercentage },
            { UserClaims.SustainablePoints, SustainablePoints },
            { UserClaims.EcoScore, EcoScore },
            { UserClaims.EcoCoins, EcoCoins },
            { UserClaims.CurrentXp, CurrentXp },
            { UserClaims.NextLevelXp, NextLevelXp },
            { UserClaims.Type, userType } 
        };

        if (!UserType.Equals(UserType.Employee))
        {
            return claims;
        }

        claims.Add(UserClaims.Role, role);
        claims.Add(UserClaims.StoreId, storeId);

        return claims;
    }
}