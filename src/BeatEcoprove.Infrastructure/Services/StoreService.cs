using BeatEcoprove.Application.Shared.Extensions;
using BeatEcoprove.Application.Shared.Helpers;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Providers;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Application.Shared.Interfaces.Services.Common;
using BeatEcoprove.Domain.ClosetAggregator.Enumerators;
using BeatEcoprove.Domain.ClosetAggregator.ValueObjects;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;
using BeatEcoprove.Domain.ProfileAggregator.Enumerators;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Errors;
using BeatEcoprove.Domain.StoreAggregator;
using BeatEcoprove.Domain.StoreAggregator.Entities;
using BeatEcoprove.Domain.StoreAggregator.Enumerators;
using BeatEcoprove.Domain.StoreAggregator.ValueObjects;

using ErrorOr;

namespace BeatEcoprove.Infrastructure.Services;

public class StoreService(
    IStoreRepository storeRepository,
    IFileStorageProvider fileStorageProvider,
    IProfileRepository profileRepository,
    IMaintenanceServiceRepository maintenanceServiceRepository,
    IPasswordGenerator passwordGenerator,
    IClosetService closetService)
    : IStoreService
{
    public async Task GivePointsTo(
        Store store,
        Profile owner,
        int sustainablePoints,
        CancellationToken cancellationToken = default)
    {
        if (owner is Organization organization)
        {
            owner.SustainabilityPoints += sustainablePoints;
        }

        store.SustainablePoints += sustainablePoints;

        var workerProfileIds = store.Workers
            .Select(worker => worker.Profile)
            .ToList();

        await profileRepository.UpdateWorkerProfileSustainablePoints(
            workerProfileIds,
            store.SustainablePoints,
            cancellationToken);
    }

    public async Task<List<Order>> GetAllStoresAsync(ProfileId owner, GetAllStoreInput input,
        CancellationToken cancellationToken = default)
    {
        var stores = await storeRepository.GetAllStoresAsync(
            owner,
            input.Search,
            input.Services,
            input.Colors,
            input.Brands,
            input.PageSize,
            input.Page,
            cancellationToken
        );

        return stores;
    }

    public async Task<ErrorOr<List<Store>>> GetOwningStoreAsync(
        Profile profile,
        GetOwningStoreInput input,
        CancellationToken cancellationToken = default)
    {
        bool isEmployee = profile.Type.Equals(UserType.Employee);

        var stores = await storeRepository.GetOwningStoreAsync(
            profile.Id,
            isEmployee,
            input.Search,
            input.Page,
            input.PageSize,
            cancellationToken);

        return stores;
    }

    public async Task<ErrorOr<Store>> GetStoreAsync(
        StoreId id,
        Profile profile,
        CancellationToken cancellationToken = default)
    {
        var isEmployee = profile.Type.Equals(UserType.Employee);

        if (!await storeRepository.HasAccessToStore(id, profile, isEmployee, cancellationToken))
        {
            return Errors.Store.DontHaveAccessToStore;
        }

        var store = await storeRepository.GetByIdAsync(id, cancellationToken);

        if (store is null)
        {
            return Errors.Store.StoreNotFound;
        }

        return store;
    }

    public async Task<ErrorOr<Store>> CreateStoreAsync(
        Store store,
        Profile profile,
        CancellationToken cancellationToken)
    {
        if (!profile.Type.Equals(UserType.Organization))
        {
            return Errors.Store.CantCreateStore;
        }

        if (await storeRepository.ExistsAnyStoreWithName(store.Name, cancellationToken))
        {
            return Errors.Store.StoreAlreadyExistsName;
        }

        store.SetPictureUrl($"https://robohash.org/{store.Id.Value.ToString()}");

        await storeRepository.AddAsync(store, cancellationToken);
        return store;
    }

    public async Task<ErrorOr<Store>> DeleteStoreAsync(Store store, Profile profile,
        CancellationToken cancellationToken = default)
    {
        if (profile.Type.Equals(UserType.Employee))
        {
            return Errors.Store.DontHaveAccessToStore;
        }

        var deleted = await storeRepository.DeleteStoreAsync(store, cancellationToken);

        if (!deleted)
        {
            return Errors.Store.StoreNotFound;
        }

        return store;
    }

    public async Task<ErrorOr<Order>> RegisterOrderAsync(
        Store store,
        ProfileId owner,
        ClothId clothId,
        CancellationToken cancellationToken = default)
    {
        if (!await profileRepository.CanProfileAccessCloth(owner, clothId, cancellationToken))
        {
            return Errors.Profile.CannotFindCloth;
        }

        var profile = await profileRepository.GetByIdAsync(owner, cancellationToken);

        if (profile is null)
        {
            return Errors.Profile.NotFound;
        }

        var cloth = await closetService.GetCloth(profile, clothId, cancellationToken);

        if (cloth.IsError)
        {
            return cloth.Errors;
        }

        var maintenance = await maintenanceServiceRepository.GetMaintenanceServiceByName(
            "Lavar",
            cancellationToken);

        if (maintenance is null)
        {
            return Errors.MaintenanceService.NotFound;
        }

        var order = store.RegisterOrderCloth(
            owner,
            clothId,
            new() { maintenance.Id }
        );

        cloth.Value.SetState(ClothState.Blocked);

        return order;
    }

    public async Task<ErrorOr<Order>> CompleteOrderAsync(
        Store store,
        OrderId orderId,
        ProfileId ownerId,
        CancellationToken cancellationToken = default)
    {
        var order = store
            .Orders
            .Where(order => order is OrderCloth)
            .FirstOrDefault(order => order.Id == orderId) as OrderCloth;

        if (order is null)
        {
            return Errors.Order.NotFound;
        }

        if (order.Status == OrderStatus.Completed)
        {
            return Errors.Order.IsAlreadyCompleted;
        }

        var owner = await profileRepository.GetByIdAsync(ownerId, cancellationToken);

        if (owner is null)
        {
            return Errors.Profile.NotFound;
        }

        var cloth = await closetService.GetCloth(
            owner,
            order.Cloth,
            cancellationToken);

        if (cloth.IsError)
        {
            return Errors.Cloth.CannotAccessBucket;
        }

        store.Complete(order);
        cloth.Value.SetState(ClothState.Idle);

        return order;
    }

    private static Task<ErrorOr<AuthId>> CreateAccount(
        string email,
        string password,
        Profile profile,
        Stream stream,
        CancellationToken cancellationToken)
    {
        var authId = AuthId.Create(Guid.NewGuid());
        profile.SetAuthId(authId);
        return Task.FromResult<ErrorOr<AuthId>>(authId);
    }
    
    public async Task<ErrorOr<(Worker, Password)>> RegisterWorkerAsync(
        Store store,
        Profile profile,
        AddWorkerInput input,
        CancellationToken cancellationToken = default)
    {
        var isEmployee = profile.Type.Equals(UserType.Employee);

        if (isEmployee)
        {
            var foundWorker = await storeRepository.GetWorkerByProfileAsync(profile.Id, cancellationToken);

            if (foundWorker is null)
            {
                return Errors.Worker.NotFound;
            }

            if (foundWorker.Role != WorkerType.Manager)
            {
                return Errors.Store.DontHaveAccessToStore;
            }
        }

        if (string.IsNullOrEmpty(input.Name))
        {
            return Errors.Worker.NotAllowedName;
        }

        if (!await storeRepository.HasAccessToStore(store.Id, profile, isEmployee, cancellationToken))
        {
            return Errors.Store.DontHaveAccessToStore;
        }

        var password = passwordGenerator.GeneratePassword(6, 16);
        var displayName = DisplayName.Create(input.Name);

        if (displayName.IsError)
        {
            return displayName.Errors;
        }

        var employee = Employee.Create(
            displayName.Value,
            "",
            "",
            "",
            profile.Phone.Clone()
        );

        var account = await CreateAccount(
            input.Email,
            password,
            employee,
            Stream.Null,
            cancellationToken
        );

        if (account.IsError)
        {
            return account.Errors;
        }

        var worker = store.AddWorker(
            employee,
            input.Type
        );

        return (worker, password);
    }

    public async Task<ErrorOr<Worker>> RemoveWorkerAsync(Store store, Profile profile, WorkerId workerId, CancellationToken cancellationToken = default)
    {
        var isEmployee = profile.Type.Equals(UserType.Employee);

        if (isEmployee)
        {
            var foundWorker = await storeRepository.GetWorkerByProfileAsync(profile.Id, cancellationToken);

            if (foundWorker is null)
            {
                return Errors.Worker.NotFound;
            }

            if (foundWorker.Role != WorkerType.Manager)
            {
                return Errors.Store.DontHaveAccessToStore;
            }
        }

        var worker = await storeRepository.GetWorkerAsync(workerId, cancellationToken);

        if (worker is null)
        {
            return Errors.Worker.NotFound;
        }

        var workerProfile = await profileRepository.GetByIdAsync(worker.Profile, cancellationToken);

        if (workerProfile is null)
        {
            return Errors.Profile.NotFound;
        }

        await profileRepository.DeleteAsync(workerProfile.Id, cancellationToken);
        await storeRepository.RemoveWorkerAsync(worker.Id, cancellationToken);

        return worker;
    }

    public async Task<ErrorOr<Worker>> SwitchPermission(Store store, Profile profile, SwitchPermissionInput input,
        CancellationToken cancellationToken = default)
    {
        var isEmployee = profile.Type.Equals(UserType.Employee);

        if (!await storeRepository.HasAccessToStore(store.Id, profile, isEmployee, cancellationToken))
        {
            return Errors.Store.DontHaveAccessToStore;
        }

        var worker = await storeRepository.GetWorkerAsync(input.WorkerId, cancellationToken);

        if (worker is null)
        {
            return Errors.Worker.NotFound;
        }

        if (isEmployee)
        {
            var foundWorker = await storeRepository.GetWorkerByProfileAsync(profile.Id, cancellationToken);

            if (foundWorker is null)
            {
                return Errors.Worker.NotFound;
            }

            if (foundWorker.Role != WorkerType.Manager)
            {
                return Errors.Store.DontHaveAccessToStore;
            }
        }

        var shouldUpgrade = store.SwitchWorkerPermission(
            worker,
            input.WorkerType
        );

        if (shouldUpgrade.IsError)
        {
            return shouldUpgrade.Errors;
        }

        return shouldUpgrade;
    }

    public ErrorOr<WorkerType> GetWorkerType(string permission)
    {
        if (permission.CanConvertToEnum(out WorkerType result))
        {
            return result;
        }

        return Errors.Worker.InvalidPermission;
    }
}