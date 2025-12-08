using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;

using ErrorOr;

namespace BeatEcoprove.Application.Profiles.Queries.GetPublicProfile;

public record GetPublicProfileQuery(
    List<Guid> Ids
) : IQuery<ErrorOr<List<Profile>>>;