using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Brands.Queries;
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

        brands.MapGet(String.Empty, async (
            ISender sender,
            IMapper mapper,
            HttpContext context
        ) =>
        {
            var result =
                await sender.Send(new GetAllBrandsQuery());

            return result.Match(
                brand => Results.Ok(
                    mapper.Map<BrandResponse>(brand)),
                errors => errors.ToProblemDetails(context)
            );
        }).RequireScopes("brand:view");
    }
}