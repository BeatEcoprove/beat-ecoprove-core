using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;

using ErrorOr;

namespace BeatEcoprove.Application.Profiles.Queries.GetMyProfiles;

public record GetMyProfilesQuery
(
    List<Guid> ProfileIds
) : IQuery<ErrorOr<List<Profile>>>;