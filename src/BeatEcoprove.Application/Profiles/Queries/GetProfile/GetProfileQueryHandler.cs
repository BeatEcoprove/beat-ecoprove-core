using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Errors;

using ErrorOr;

namespace BeatEcoprove.Application.Profiles.Queries.GetProfile;

internal sealed class GetProfileQueryHandler(
    IProfileManager profileManager, 
    IProfileRepository profileRepository)
    : IQueryHandler<GetProfileQuery, ErrorOr<Profile>>
{
    public async Task<ErrorOr<Profile>> Handle(
        GetProfileQuery request, 
        CancellationToken cancellationToken)
    {
        var profileId = ProfileId.Create(request.ProfileId);
        var profile = await profileManager.GetProfileAsync(profileId, cancellationToken);

        return profile.IsError ? profile.Errors : profile;
    }
}