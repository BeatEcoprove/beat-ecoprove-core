using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Errors;
using BeatEcoprove.Domain.StoreAggregator.Daos;
using BeatEcoprove.Domain.StoreAggregator.ValueObjects;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Queries.GetWorkers;

internal sealed class GetWorkersQueryHandler : IQueryHandler<GetWorkersQuery, ErrorOr<List<WorkerDao>>>
{
    private readonly IProfileManager _profileManager;
    private readonly IStoreRepository _storeRepository;

    public GetWorkersQueryHandler(IProfileManager profileManager, IStoreRepository storeRepository)
    {
        _profileManager = profileManager;
        _storeRepository = storeRepository;
    }

    public async Task<ErrorOr<List<WorkerDao>>> Handle(GetWorkersQuery request, CancellationToken cancellationToken)
    {
        var profileId = ProfileId.Create(request.ProfileId);
        var storeId = StoreId.Create(request.StoreId);

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

        var workers = await _storeRepository.GetWorkerDaosAsync(
            store.Id,
            request.Search,
            request.Page,
            request.PageSize,
            cancellationToken);

        return workers;
    }
}