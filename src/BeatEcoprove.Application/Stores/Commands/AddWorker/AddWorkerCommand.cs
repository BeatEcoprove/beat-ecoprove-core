using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.StoreAggregator.Daos;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Commands.AddWorker;

public record AddWorkerCommand
(
    Guid ProfileId,
    Guid StoreId,
    string Name,
    string Email,
    string Permission
) : ICommand<ErrorOr<WorkerDao>>;