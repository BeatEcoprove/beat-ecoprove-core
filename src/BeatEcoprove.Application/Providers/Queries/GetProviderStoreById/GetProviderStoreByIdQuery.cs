using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.StoreAggregator;

using ErrorOr;

namespace BeatEcoprove.Application.Providers.Queries.GetProviderStoreById;

public record GetProviderStoreByIdQuery
(
    Guid ProfileId,
    Guid ProviderId,
    Guid StoreId
) : IQuery<ErrorOr<Store>>;