using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Providers;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.AdvertisementAggregator;
using BeatEcoprove.Domain.AdvertisementAggregator.ValueObjects;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;
using BeatEcoprove.Domain.ProfileAggregator.Enumerators;
using BeatEcoprove.Domain.Shared.Errors;
using BeatEcoprove.Domain.StoreAggregator.Entities;
using BeatEcoprove.Domain.StoreAggregator.Enumerators;
using BeatEcoprove.Domain.StoreAggregator.ValueObjects;

using ErrorOr;

namespace BeatEcoprove.Infrastructure.Services;

public class AdvertisementService(
    IStoreRepository storeRepository,
    IAdvertisementRepository advertisementRepository,
    IFileStorageProvider fileProvider,
    IStoreService storeService,
    IProfileRepository profileRepository)
    : IAdvertisementService
{
    public async Task<ErrorOr<Advertisement>> GetAdvertAsync(
        AdvertisementId advertId,
        Profile profile,
        bool checkAuthorization = true,
        CancellationToken cancellationToken = default)
    {
        var isEmployee = profile.Type.Equals(UserType.Employee);
        var advert = await advertisementRepository.GetByIdAsync(advertId, cancellationToken);

        if (advert is null)
        {
            return Errors.Advertisement.NotFound;
        }

        if (!checkAuthorization)
        {
            return advert;
        }

        if (!await advertisementRepository.HasProfileAccessToAdvert(advertId, profile.Id, isEmployee, cancellationToken))
        {
            return Errors.Advertisement.CannotPerformThis;
        }

        return advert;
    }

    public async Task<ErrorOr<List<Advertisement>>> GetMyAdvertsAsync(
        StoreId storeId,
        Profile profile,
        string? search = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var isEmployee = profile.Type.Equals(UserType.Employee);

        var adverts = await advertisementRepository.GetAllAddsAsync(
           profile.Id,
           isEmployee,
           search: search,
           page: page,
           pageSize: pageSize,
           cancellationToken: cancellationToken
       );

        return adverts;
    }

    public async Task<ErrorOr<Advertisement>> CreateAdd(
        StoreId storeId,
        Advertisement advertisement,
        Profile profile,
        CancellationToken cancellationToken = default)
    {
        if (advertisement.InitDate >= advertisement.EndDate)
        {
            return Errors.Advertisement.DateMustBeValid;
        }

        if (profile.Type.Equals(UserType.Consumer))
        {
            return Errors.Advertisement.CannotPerformThis;
        }

        if (profile.Type.Equals(UserType.Employee))
        {
            var result = await DoIfEmployee(profile, cancellationToken);

            if (result.IsError)
            {
                return result.Errors;
            }

            storeId = result.Value.Store;
        }

        if (profile.SustainabilityPoints < advertisement.SustainablePoints)
        {
            return Errors.Advertisement.DontHaveEnoughPoint;
        }

        if (storeId.Value != Guid.Empty)
        {
            var store = await storeService.GetStoreAsync(storeId, profile, cancellationToken);

            if (store.IsError)
            {
                return store.Errors;
            }

            advertisement.SetStore(storeId);
            advertisement.SetMainProfile(store.Value.Owner);

            store.Value.SustainablePoints -= advertisement.SustainablePoints;

            var ownerProfile = await profileRepository.GetByIdAsync(store.Value.Owner, cancellationToken);

            if (ownerProfile is null)
            {
                return Errors.Provider.NotFound;
            }

            ownerProfile.SustainabilityPoints -= advertisement.SustainablePoints;
        }
        else
        {
            await storeRepository.SubtractPoints(
                profile.Id,
                advertisement.SustainablePoints,
                cancellationToken);
        }

        profile.SustainabilityPoints -= advertisement.SustainablePoints;
        advertisement.SetPictureUrl($"https://robohash.org/{advertisement.Id.Value.ToString()}");

        await advertisementRepository.AddAsync(advertisement, cancellationToken);

        return advertisement;
    }

    public async Task<ErrorOr<Advertisement>> DeleteAsync(
        Advertisement advertisement,
        Profile profile,
        CancellationToken cancellationToken = default)
    {
        var isEmployee = profile.Type.Equals(UserType.Employee);

        if (!await advertisementRepository.HasProfileAccessToAdvert(
                advertisement.Id,
                profile.Id,
                isEmployee,
                cancellationToken))
        {
            return Errors.Advertisement.CannotPerformThis;
        }

        if (isEmployee)
        {
            var worker = await storeRepository.GetWorkerByProfileAsync(profile.Id, cancellationToken);

            if (worker is null)
            {
                return Errors.Worker.NotFound;
            }

            if (worker.Role != WorkerType.Manager)
            {
                return Errors.Store.DontHaveAccessToStore;
            }
        }

        await advertisementRepository.RemoveAsync(advertisement.Id, cancellationToken);
        return advertisement;
    }

    private async Task<ErrorOr<Worker>> DoIfEmployee(Profile profile, CancellationToken cancellationToken)
    {
        var worker = await storeRepository.GetWorkerByProfileAsync(profile.Id, cancellationToken);

        if (worker is null)
        {
            return Errors.Worker.NotFound;
        }

        if (worker.Role != WorkerType.Manager)
        {
            return Errors.Advertisement.CannotPerformThis;
        }

        return worker;
    }
}