using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;

using ErrorOr;

namespace BeatEcoprove.Application.Profiles.Queries.GetMyProfiles;

internal sealed class GetMyProfilesQueryHandler(
    IProfileRepository profileRepository
) : IQueryHandler<GetMyProfilesQuery, ErrorOr<List<Profile>>>
{
    public async Task<ErrorOr<List<Profile>>> Handle(GetMyProfilesQuery request, CancellationToken cancellationToken)
    {
        var profiles = new List<Profile>();

        foreach (var id in request.ProfileIds)
        {
            var profileId = ProfileId.Create(id);
            var profile = await profileRepository.GetByIdAsync(profileId, cancellationToken);

            if (profile is not null)
                profiles.Add(profile);
        }


        return profiles;
    }
}