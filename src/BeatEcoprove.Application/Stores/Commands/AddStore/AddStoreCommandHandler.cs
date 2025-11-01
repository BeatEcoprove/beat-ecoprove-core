using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Interfaces.Persistence;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.StoreAggregator;

using ErrorOr;

using NetTopologySuite.Geometries;

namespace BeatEcoprove.Application.Stores.Commands.AddStore;

internal sealed class AddStoreCommandHandler(
    IProfileManager profileManager,
    IStoreService storeService,
    IStoreRepository storeRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddStoreCommand, ErrorOr<Store>>
{
    private readonly IStoreRepository _storeRepository = storeRepository;

    public async Task<ErrorOr<Store>> Handle(AddStoreCommand request, CancellationToken cancellationToken)
    {
        var profileId = ProfileId.Create(request.ProfileId);
        var postalCode = PostalCode.Create(request.PostalCode);

        if (postalCode.IsError)
        {
            return postalCode.Errors;
        }

        var address = Address.Create(
            request.Street,
            request.Port,
            request.Locality,
            postalCode.Value
        );

        if (address.IsError)
        {
            return address.Errors;
        }

        var profile = await profileManager.GetProfileAsync(profileId, cancellationToken);

        if (profile.IsError)
        {
            return profile.Errors;
        }

        var store = Store.Create(
            profile.Value.Id,
            request.Name,
            address.Value,
            new Point(new Coordinate(request.Lat, request.Lon))
        );

        var storeResult = await storeService.CreateStoreAsync(
            store,
            profile.Value,
            cancellationToken
        );

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return storeResult;
    }
}