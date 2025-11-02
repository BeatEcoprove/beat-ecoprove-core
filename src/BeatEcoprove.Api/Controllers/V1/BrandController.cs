using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Brands.Queries;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Contracts.Brands;

using MapsterMapper;

using MediatR;

namespace BeatEcoprove.Api.Controllers.V1;

public class BrandController : ApiCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var brands = CreateVersionedGroup(app, "extensions/brands")
            .WithName("Brands")
            .RequireAuthorization();

        brands.MapGet(String.Empty, async (
            ISender sender,
            IMapper mapper,
            ILanguageCulture localizer
        ) =>
        {
            var result =
                await sender.Send(new GetAllBrandsQuery());

            return result.Match(
                brand => Results.Ok(
                    mapper.Map<BrandResponse>(brand)),
                errors => errors.ToProblemDetails(localizer)
            );
        }).RequireScopes("brands:view");
    }
}