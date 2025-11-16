using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Brands.Commands;
using BeatEcoprove.Application.Brands.Queries;
using BeatEcoprove.Application.ImageUpload.Commands;
using BeatEcoprove.Contracts.Brands;

using MapsterMapper;

using MediatR;

namespace BeatEcoprove.Api.Controllers.V1;

public class BrandController : ApiCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var brands = CreateVersionedGroup(app, "extensions/brands")
            .WithTags("Brands")
            .RequireAuthorization();

        brands.MapGet(string.Empty, GetAllBrands).RequireScopes("brand:view");
        brands.MapPost(string.Empty, CreateBrand).RequireScopes("brand:create");
    }

    private static async Task<IResult> GetAllBrands(
        ISender sender,
        IMapper mapper,
        HttpContext context
    )
    {
        var result =
            await sender.Send(new GetAllBrandsQuery());

        return result.Match(
            brand => Results.Ok(
                mapper.Map<BrandResponse>(brand)),
            errors => errors.ToProblemDetails(context)
        );
    }

    private static async Task<IResult> CreateBrand(
        ISender sender,
        IMapper mapper,
        CreateBrandRequest request,
        HttpContext context
    )
    {
        var result =
            await sender.Send(new CreateBrandCommand(
                request.Name,
                request.Picture
            ));

        return result.Match(
            brand => Results.Ok(mapper.Map<BrandResult>(brand)),
            errors => errors.ToProblemDetails(context)
        );
    }
}