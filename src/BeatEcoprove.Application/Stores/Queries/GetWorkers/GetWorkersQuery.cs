using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.StoreAggregator.Daos;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Queries.GetWorkers;

public record GetWorkersQuery
(
    Guid ProfileId,
    Guid StoreId,
    string? Search = null,
    int Page = 1,
    int PageSize = 10
) : IQuery<ErrorOr<List<WorkerDao>>>;