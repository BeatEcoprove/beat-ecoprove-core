using BeatEcoprove.Application.Closet.Common;
using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Extensions;
using BeatEcoprove.Application.Shared.Interfaces.Persistence;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.ClosetAggregator;
using BeatEcoprove.Domain.Shared.Errors;
using BeatEcoprove.Domain.Shared.Extensions;

using ErrorOr;

namespace BeatEcoprove.Application.Closet.Commands.CreateCloth;

public class CreateClothCommandHandler(
    IUnitOfWork unitOfWork,
    IColorRepository colorRepository,
    IProfileManager profileManager,
    IClosetService closetService,
    IBrandRepository brandRepository)
    : ICommandHandler<CreateClothCommand, ErrorOr<ClothResult>>
{
    public async Task<ErrorOr<ClothResult>> Handle(
        CreateClothCommand request,
        CancellationToken cancellationToken)
    {
        var profile = await profileManager.GetProfileAsync(request.ProfileId, cancellationToken);

        if (profile.IsError)
        {
            return profile.Errors;
        }

        var colorId = await colorRepository.GetByHexValueAsync(request.Color, cancellationToken);

        if (colorId is null)
        {
            return Errors.Color.BadHexValue;
        }

        var brandId = await brandRepository.GetBrandIdByNameAsync(request.Brand, cancellationToken);

        if (brandId is null)
        {
            return Errors.Brand.ThereIsNoBrandName;
        }

        var clothType = closetService.GetClothType(request.ClothType);
        var clothSize = closetService.GetClothSize(request.ClothSize);

        var shouldBeValidTypes = clothType.AddValidate(clothSize);

        if (shouldBeValidTypes.IsError)
        {
            return shouldBeValidTypes.Errors;
        }

        var cloth = Cloth.Create
        (
            request.Name.Capitalize(),
            clothType.Value,
            clothSize.Value,
            brandId,
            colorId
        );

        if (cloth.IsError)
        {
            return cloth.Errors;
        }

        var clothResult = await closetService.AddClothToCloset(
            profile.Value,
            cloth.Value,
            request.Brand,
            request.Color,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return clothResult;
    }
}