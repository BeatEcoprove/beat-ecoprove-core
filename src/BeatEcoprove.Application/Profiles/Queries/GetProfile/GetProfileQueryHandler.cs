using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Gaming;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;

using ErrorOr;

namespace BeatEcoprove.Application.Profiles.Queries.GetProfile;

internal sealed class GetProfileQueryHandler(
    IProfileManager profileManager,
    IGamingService gamingService)
    : IQueryHandler<GetProfileQuery, ErrorOr<GamificationDTO>>
{
    public async Task<ErrorOr<GamificationDTO>> Handle(
        GetProfileQuery request,
        CancellationToken cancellationToken)
    {
        var profileId = ProfileId.Create(request.ProfileId);
        var profile = await profileManager.GetProfileAsync(profileId, cancellationToken);

        if (profile.IsError)
            return profile.Errors;

        var nextLevelUp = gamingService.GetNextLevelXp(profile.Value);
        return new GamificationDTO(
            profile.Value,
            nextLevelUp
        );
    }
}