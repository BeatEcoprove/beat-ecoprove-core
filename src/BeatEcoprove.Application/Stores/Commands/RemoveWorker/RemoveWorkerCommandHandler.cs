using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Interfaces.Persistence;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Errors;
using BeatEcoprove.Domain.StoreAggregator.Daos;
using BeatEcoprove.Domain.StoreAggregator.ValueObjects;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Commands.RemoveWorker;

internal sealed class RemoveWorkerCommandHandler : ICommandHandler<RemoveWorkerCommand, ErrorOr<WorkerDao>>
{
    private readonly IProfileManager _profileManager;
    private readonly IStoreRepository _storeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStoreService _storeService;

    public RemoveWorkerCommandHandler(
        IProfileManager profileManager,
        IStoreRepository storeRepository,
        IUnitOfWork unitOfWork,
        IStoreService storeService)
    {
        _profileManager = profileManager;
        _storeRepository = storeRepository;
        _unitOfWork = unitOfWork;
        _storeService = storeService;
    }

    public async Task<ErrorOr<WorkerDao>> Handle(RemoveWorkerCommand request, CancellationToken cancellationToken)
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

        var workerDao = await _storeRepository.GetWorkerDaoAsync(workerId, cancellationToken);

        if (workerDao is null)
        {
            return Errors.Worker.NotFound;
        }

        var removedWorker = await _storeService.RemoveWorkerAsync(
            store,
            profile.Value,
            workerId,
            cancellationToken);

        if (removedWorker.IsError)
        {
            return removedWorker.Errors;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return workerDao;
    }
}