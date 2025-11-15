using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Gaming;

using ErrorOr;

namespace BeatEcoprove.Application.Profiles.Queries.GetProfile;

public record GetProfileQuery
(
    Guid ProfileId
) : IQuery<ErrorOr<GamificationDTO>>;