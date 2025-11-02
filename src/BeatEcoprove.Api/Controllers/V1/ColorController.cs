using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Colors.Queries;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Contracts.Colors;

using MapsterMapper;

using MediatR;

namespace BeatEcoprove.Api.Controllers.V1;

public class ColorController : ApiCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var colors = CreateVersionedGroup(app, "extensions/colors")
            .WithName("Colors")
            .RequireAuthorization();

        colors.MapGet(String.Empty, async (
            ISender sender,
            IMapper mapper,
            ILanguageCulture localizer
        ) =>
        {
            var result =
                await sender.Send(new GetColorsQuery());

            return result.Match(
                color => Results.Ok(
                    mapper.Map<ColorResponse>(color)),
                errors => errors.ToProblemDetails(localizer)
            );
        }).RequireScopes("color:view");
    }
}