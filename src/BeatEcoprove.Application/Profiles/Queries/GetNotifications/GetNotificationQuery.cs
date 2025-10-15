using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Helpers;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Notifications;

using ErrorOr;

namespace BeatEcoprove.Application.Profiles.Queries.GetNotifications;

public record GetNotificationQuery
(
    Guid ProfileId
) : IQuery<ErrorOr<List<Notification>>>, IAuthorization
{
    Guid IAuthorization.ProfileId => ProfileId;
    Guid IAuthorization.AuthId => ProfileId;
}