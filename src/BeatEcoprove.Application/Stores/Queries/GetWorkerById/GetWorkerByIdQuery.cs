using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.StoreAggregator.Daos;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Queries.GetWorkerById;

public record GetWorkerByIdQuery
(
    Guid ProfileId,
    Guid StoreId,
    Guid WorkerId
) : IQuery<ErrorOr<WorkerDao>>;