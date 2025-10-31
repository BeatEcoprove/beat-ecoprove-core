using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Services.Queries.GetAllServices;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Contracts.Services;

using MapsterMapper;

using MediatR;

namespace BeatEcoprove.Api.Controllers.V1;

public class ServicesController : ApiCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var services = CreateVersionedGroup(app, "services")
            .RequireAuthorization();

        services.MapGet(string.Empty, async (
            ISender sender,
            IMapper mapper,
            ILanguageCulture localizer,
            HttpContext context,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await sender.Send(new GetAllServicesQuery(), cancellationToken);

            return result.Match(
                response => Results.Ok(
                    mapper.Map<List<MaintenanceServiceResponse>>(response)),
                errors => errors.ToProblemDetails(localizer)
            );
        });
    }
}