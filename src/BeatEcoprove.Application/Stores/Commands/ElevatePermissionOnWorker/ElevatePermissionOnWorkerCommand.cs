using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.StoreAggregator.Daos;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Commands.ElevatePermissionOnWorker;

public record ElevatePermissionOnWorkerCommand
(
    Guid ProfileId,
    Guid StoreId,
    Guid WorkerId,
    string Permission
) : ICommand<ErrorOr<WorkerDao>>;