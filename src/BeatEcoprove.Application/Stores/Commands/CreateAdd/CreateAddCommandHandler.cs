using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Interfaces.Persistence;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.AdvertisementAggregator;
using BeatEcoprove.Domain.AdvertisementAggregator.Entities;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Errors;
using BeatEcoprove.Domain.StoreAggregator.ValueObjects;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Commands.CreateAdd;

internal sealed class CreateAddCommandHandler(
    IProfileManager profileManager,
    IAdvertisementService advertisementService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateAddCommand, ErrorOr<Advertisement>>
{
    public async Task<ErrorOr<Advertisement>> Handle(CreateAddCommand request, CancellationToken cancellationToken)
    {
        var profileId = ProfileId.Create(request.ProfileId);
        var storeId = StoreId.Create(request.StoreId);

        var profile = await profileManager.GetProfileAsync(profileId, cancellationToken);

        if (profile.IsError)
        {
            return profile.Errors;
        }

        var beginAt = request.BeginAt.ToDateTime(TimeOnly.MaxValue, DateTimeKind.Utc);
        var endAt = request.EndAt.ToDateTime(TimeOnly.MaxValue, DateTimeKind.Utc);

        ErrorOr<Advertisement> advertisement = request.Type switch
        {
            "advertisement" => Advertisement.Create(
                profile.Value.Id,
                request.Title,
                request.Description,
                beginAt,
                endAt
            ),
            "promotion" => Promotion.Create(
                profile.Value.Id,
                request.Title,
                request.Description,
                beginAt,
                endAt
            ),
            "voucher" => Voucher.Create(
                profile.Value.Id,
                request.Title,
                request.Description,
                beginAt,
                endAt,
                request.Quantity
            ),
            _ => Errors.Advertisement.NotFound
        };

        if (advertisement.IsError)
        {
            return advertisement.Errors;
        }

        var createResult = await advertisementService.CreateAdd(
            storeId,
            advertisement.Value,
            profile.Value,
            cancellationToken
        );

        if (createResult.IsError)
        {
            return createResult.Errors;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return advertisement;
    }
}