using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.AdvertisementAggregator;

using ErrorOr;

namespace BeatEcoprove.Application.Providers.Queries.GetProviderAdverts;

public record GetProviderAdvertsQuery
(
    Guid ProfileId,
    Guid ProviderId,
    string? Search = null,
    int Page = 1,
    int PageSize = 10
) : IQuery<ErrorOr<List<Advertisement>>>;