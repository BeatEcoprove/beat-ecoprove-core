using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.StoreAggregator.Daos;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Commands.RemoveWorker;

public record RemoveWorkerCommand
(
    Guid ProfileId,
    Guid StoreId,
    Guid WorkerId
) : ICommand<ErrorOr<WorkerDao>>;