using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.ProfileAggregator.DAOS;

using ErrorOr;

namespace BeatEcoprove.Application.Profiles.Queries.GetMyProfiles;

internal sealed class GetMyProfilesQueryHandler : IQueryHandler<GetMyProfilesQuery, ErrorOr<List<ProfileDao>>>
{
    private readonly IProfileManager _profileManager;
    private readonly IProfileRepository _profileRepository;

    public GetMyProfilesQueryHandler(
        IProfileManager profileManager,
        IProfileRepository profileRepository)
    {
        _profileManager = profileManager;
        _profileRepository = profileRepository;
    }

    public async Task<ErrorOr<List<ProfileDao>>> Handle(GetMyProfilesQuery request, CancellationToken cancellationToken)
    {
        var profile = await _profileManager.GetProfileAsync(Guid.Empty, cancellationToken);

        if (profile.IsError)
        {
            return profile.Errors;
        }

        var profileList = await _profileRepository.GetAllProfilesAsync(
            null, // No search filter
            1,    // First page
            100,  // Maximum profiles to return
            cancellationToken);

        return profileList.ConvertAll(p => p.ToDao());
    }
}