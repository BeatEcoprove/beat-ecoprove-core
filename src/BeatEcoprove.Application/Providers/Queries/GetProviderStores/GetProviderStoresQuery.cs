using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.StoreAggregator;

using ErrorOr;

namespace BeatEcoprove.Application.Providers.Queries.GetProviderStores;

public record GetProviderStoresQuery
(
    Guid ProfileId,
    Guid ProviderId,
    string? Search = null,
    int Page = 1,
    int PageSize = 10
) : IQuery<ErrorOr<List<Store>>>;