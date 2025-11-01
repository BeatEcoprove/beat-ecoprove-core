using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Errors;
using BeatEcoprove.Domain.StoreAggregator.Daos;
using BeatEcoprove.Domain.StoreAggregator.ValueObjects;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Queries.GetWorkerById;

internal sealed class GetWorkerByIdQueryHandler : IQueryHandler<GetWorkerByIdQuery, ErrorOr<WorkerDao>>
{
    private readonly IProfileManager _profileManager;
    private readonly IStoreRepository _storeRepository;

    public GetWorkerByIdQueryHandler(IProfileManager profileManager, IStoreRepository storeRepository)
    {
        _profileManager = profileManager;
        _storeRepository = storeRepository;
    }

    public async Task<ErrorOr<WorkerDao>> Handle(GetWorkerByIdQuery request, CancellationToken cancellationToken)
    {
        var profileId = ProfileId.Create(request.ProfileId);
        var storeId = StoreId.Create(request.StoreId);
        var workerId = WorkerId.Create(request.WorkerId);

        var profile = await _profileManager.GetProfileAsync(profileId, cancellationToken);

        if (profile.IsError)
        {
            return profile.Errors;
        }

        var store = await _storeRepository.GetByIdAsync(storeId, cancellationToken);

        if (store is null)
        {
            return Errors.Store.StoreNotFound;
        }

        var worker = store.Workers.FirstOrDefault(worker => worker.Id == workerId);

        if (worker is null)
        {
            return Errors.Worker.NotFound;
        }

        var workerDao = await _storeRepository.GetWorkerDaoAsync(worker.Id, cancellationToken);

        if (workerDao is null)
        {
            return Errors.Worker.NotFound;
        }

        return workerDao;
    }
}