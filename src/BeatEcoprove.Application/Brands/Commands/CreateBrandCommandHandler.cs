using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Interfaces.Persistence;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Domain.Shared.Entities;

using ErrorOr;
using BeatEcoprove.Application.Shared.Interfaces.Services;

namespace BeatEcoprove.Application.Brands.Commands;

internal sealed class CreateBrandCommandHandler(
    IBrandRepository brandRepository,
    IPictureService pictureService,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateBrandCommand, ErrorOr<Brand>>
{
    public async Task<ErrorOr<Brand>> Handle(
        CreateBrandCommand request,
        CancellationToken cancellationToken
    )
    {
        var imageUrl = await pictureService.GetImageUrlLiteral(
            request.Picture,
            cancellationToken);

        if (imageUrl.IsError)
            return imageUrl.Errors;

        var brand = Brand.Create(
            request.Name,
            imageUrl.Value
        );

        if (brand.IsError)
            return brand.Errors;

        await brandRepository.AddAsync(brand.Value, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return brand;
    }
}