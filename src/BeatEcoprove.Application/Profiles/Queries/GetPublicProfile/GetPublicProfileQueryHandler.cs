using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;

using ErrorOr;

namespace BeatEcoprove.Application.Profiles.Queries.GetPublicProfile;

internal sealed class GetPublicProfileQueryHandler(
    IProfileRepository profileRepository) : IQueryHandler<GetPublicProfileQuery, ErrorOr<List<Profile>>>
{
    public async Task<ErrorOr<List<Profile>>> Handle(
        GetPublicProfileQuery request, 
        CancellationToken cancellationToken)
        => await profileRepository.GetAllProfilesAsync(request.Ids, cancellationToken);
}