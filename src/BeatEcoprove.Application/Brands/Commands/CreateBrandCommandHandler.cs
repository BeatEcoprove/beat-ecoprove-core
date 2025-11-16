using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Interfaces.Persistence;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Providers;
using BeatEcoprove.Domain.Shared.Entities;
using BeatEcoprove.Domain.Shared.Errors;

using ErrorOr;
using BeatEcoprove.Application.Shared.Interfaces.Helpers;
namespace BeatEcoprove.Application.Brands.Commands;

internal sealed class CreateBrandCommandHandler(
    IBrandRepository brandRepository,
    IFileStorageProvider fileStorageProvider,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateBrandCommand, ErrorOr<Brand>>
{
    public async Task<ErrorOr<Brand>> Handle(
        CreateBrandCommand request,
        CancellationToken cancellationToken
    )
    {
        var image = ImageUrl.Create(request.Picture);

        if (image.IsError)
            return image.Errors;

        if (!await fileStorageProvider.IsAlreadyCreated(
                image.Value.Bucket,
                image.Value.Id, 
                cancellationToken))
            return Errors.Brand.MustProvideBrandAvatar;

        var brand = Brand.Create(
            request.Name,
            image.Value.Url
        );

        if (brand.IsError)
            return brand.Errors;

        await brandRepository.AddAsync(brand.Value, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return brand;
    }
}