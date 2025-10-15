using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;

using ErrorOr;

namespace BeatEcoprove.Application.Shared.Interfaces.Services;

public interface IProfileManager
{
    Task<ErrorOr<Profile>> GetProfileAsync(Guid profileId,
        CancellationToken cancellationToken = default);

    Task<ErrorOr<List<ProfileId>>> GetNestedProfileIds(Guid mainProfileId,
        CancellationToken cancellationToken = default);
}