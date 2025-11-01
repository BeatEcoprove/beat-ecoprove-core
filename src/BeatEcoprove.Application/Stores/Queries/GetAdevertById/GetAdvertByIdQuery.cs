using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.AdvertisementAggregator;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Queries.GetAdevertById;

public record GetAdvertByIdQuery
(
    Guid ProfileId,
    Guid AdvertisementId,
    bool CheckAuthorization = true
) : IQuery<ErrorOr<Advertisement>>;