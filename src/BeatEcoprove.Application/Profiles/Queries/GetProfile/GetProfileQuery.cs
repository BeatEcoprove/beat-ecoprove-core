using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;

using ErrorOr;

namespace BeatEcoprove.Application.Profiles.Queries.GetProfile;

public record GetProfileQuery
(
    Guid ProfileId
) : IQuery<ErrorOr<Profile>>;