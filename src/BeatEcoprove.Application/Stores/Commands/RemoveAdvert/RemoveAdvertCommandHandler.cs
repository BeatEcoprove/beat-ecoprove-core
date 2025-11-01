using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Interfaces.Persistence;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.AdvertisementAggregator;
using BeatEcoprove.Domain.AdvertisementAggregator.ValueObjects;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Errors;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Commands.RemoveAdvert;

internal sealed class RemoveAdvertCommandHandler(
    IProfileManager profileManager,
    IAdvertisementService advertisementService,
    IAdvertisementRepository advertisementRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveAdvertCommand, ErrorOr<Advertisement>>
{
    public async Task<ErrorOr<Advertisement>> Handle(RemoveAdvertCommand request, CancellationToken cancellationToken)
    {
        var profileId = ProfileId.Create(request.ProfileId);
        var advertId = AdvertisementId.Create(request.AdvertId);

        var profile = await profileManager.GetProfileAsync(profileId, cancellationToken);

        if (profile.IsError)
        {
            return profile.Errors;
        }

        var advert = await advertisementRepository.GetByIdAsync(advertId, cancellationToken);

        if (advert is null)
        {
            return Errors.Advertisement.NotFound;
        }

        var deleteResult = await advertisementService.DeleteAsync(
            advert,
            profile.Value,
            cancellationToken);

        if (deleteResult.IsError)
        {
            return deleteResult.Errors;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return advert;
    }
}