using System.Security.Claims;

using BeatEcoprove.Application.Shared.Helpers;

namespace BeatEcoprove.Api.Extensions;

public static class ClaimsExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal claims)
    {
        var claimList = claims.Claims;

        var userId = claimList
            .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);

        return Guid.Parse(userId!.Value);
    }

    public static Guid GetProfileId(this ClaimsPrincipal claims)
    {
        var claimList = claims.Claims;

        var userId = claimList
            .FirstOrDefault(claim => claim.Type == UserClaims.ProfileId);

        return Guid.Parse(userId!.Value);
    }

    public static List<Guid> GetProfileIds(this ClaimsPrincipal claims)
    {
        if (claims?.Claims == null)
        {
            return Enumerable.Empty<Guid>().ToList();
        }

        var profileIdClaims = claims.Claims
            .Where(claim => claim.Type == UserClaims.ProfileIds)
            .Select(claim => claim.Value)
            .Where(value => !string.IsNullOrWhiteSpace(value));

        var profileIds = new List<Guid>();
        foreach (var value in profileIdClaims)
        {
            if (Guid.TryParse(value, out var guid))
            {
                profileIds.Add(guid);
            }
        }

        return profileIds;
    }

    public static string GetUserType(this ClaimsPrincipal claims)
    {
        var claimList = claims.Claims;

        var userType = claimList
            .FirstOrDefault(claim => claim.Type == UserClaims.Role)!;

        return userType.Value;
    }

    public static string GetEmail(this ClaimsPrincipal claims)
    {
        var claimList = claims.Claims;

        var email = claimList
            .FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

        return email!.Value;
    }
}