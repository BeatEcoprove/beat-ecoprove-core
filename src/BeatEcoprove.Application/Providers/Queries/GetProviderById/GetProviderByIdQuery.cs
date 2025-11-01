using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.ProfileAggregator.DAOS;

using ErrorOr;

namespace BeatEcoprove.Application.Providers.Queries.GetProviderById;

public record GetProviderByIdQuery
(
    Guid ProfileId,
    Guid ProviderId
) : IQuery<ErrorOr<ProviderDao>>;