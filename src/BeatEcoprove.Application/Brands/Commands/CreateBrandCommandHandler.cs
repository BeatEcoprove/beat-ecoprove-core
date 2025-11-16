
using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Interfaces.Persistence;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Domain.Shared.Entities;
using BeatEcoprove.Domain.Shared.ValueObjects;

using ErrorOr;

namespace BeatEcoprove.Application.Brands.Commands;

internal sealed class CreateBrandCommandHandler(
    IBrandRepository brandRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateBrandCommand, ErrorOr<Brand>>
{
    public async Task<ErrorOr<Brand>> Handle(
        CreateBrandCommand request,
        CancellationToken cancellationToken
    )
    {
        var brand = Brand.Create(
            request.Name,
            $"https://robohash.org/{BrandId.CreateUnique().Value.ToString()}"
        );

        if (brand.IsError)
            return brand.Errors;

        await brandRepository.AddAsync(brand.Value, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return brand;
    }
}