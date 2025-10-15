using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Errors;

using ErrorOr;

namespace BeatEcoprove.Infrastructure.Services;

public class ProfileManager : IProfileManager
{
    private readonly IProfileRepository _profileRepository;

    public ProfileManager(
        IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }

    public async Task<ErrorOr<Profile>> GetProfileAsync(Guid profileId, CancellationToken cancellationToken = default)
    {
        var profile = await _profileRepository.GetByIdAsync(ProfileId.Create(profileId), cancellationToken);

        if (profile is null)
        {
            return Errors.User.ProfileDoesNotExists;
        }

        return profile;
    }

    public async Task<ErrorOr<List<ProfileId>>> GetNestedProfileIds(Guid mainProfileId, CancellationToken cancellationToken = default)
    {
        var profileId = ProfileId.Create(mainProfileId);
        var profile = await _profileRepository.GetByIdAsync(profileId, cancellationToken);

        if (profile is null)
        {
            return Errors.User.ProfileDoesNotExists;
        }

        // Return all nested profileIds
        return await _profileRepository.GetNestedProfileIds(profileId, cancellationToken);
    }
}