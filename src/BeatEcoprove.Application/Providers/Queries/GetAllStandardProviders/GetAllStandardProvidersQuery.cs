using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;

using ErrorOr;

namespace BeatEcoprove.Application.Providers.Queries.GetAllStandardProviders;

public record GetAllStandardProvidersQuery
(
    Guid ProfileId,
    string? Search = null,
    int Page = 1,
    int PageSize = 10
) : IQuery<ErrorOr<List<Organization>>>;