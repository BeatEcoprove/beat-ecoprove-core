using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.StoreAggregator.Daos;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Queries.GetStoreRatings;

public record GetStoreRatingsQuery
(
    Guid ProfileId,
    Guid StoreId,
    int Page = 1,
    int PageSize = 10
) : IQuery<ErrorOr<List<RatingDao>>>;