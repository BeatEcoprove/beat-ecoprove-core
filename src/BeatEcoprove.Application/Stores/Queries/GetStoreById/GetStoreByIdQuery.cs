using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.StoreAggregator;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Queries.GetStoreById;

public record GetStoreByIdQuery
(
    Guid ProfileId,
    Guid StoreId
) : IQuery<ErrorOr<Store>>;