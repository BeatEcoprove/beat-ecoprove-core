using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;

using ErrorOr;

namespace BeatEcoprove.Domain.ProfileAggregator.Factories;

public interface IProfileFactory
{
    ErrorOr<Profile> CreateProfile(
        ProfileDetails details,
        ProfileSpecificDetails profileSpecific
    );
}