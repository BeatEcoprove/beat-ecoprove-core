using Asp.Versioning;

using BeatEcoprove.Application.Services.Queries.GetAllServices;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Contracts.Services;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeatEcoprove.Api.Controllers;

[ApiVersion(1)]
[Authorize]
[Route("v{version:apiVersion}/services")]
public class ServicesController(
    ISender sender,
    IMapper mapper,
    ILanguageCulture languageCulture)
    : ApiController(languageCulture)
{
    [HttpGet]
    public async Task<ActionResult<List<MaintenanceServiceResponse>>> GetAllServices()
    {
        var services = await sender.Send(new GetAllServicesQuery());

        return services.Match(
            result => mapper.Map<List<MaintenanceServiceResponse>>(result),
            Problem<List<MaintenanceServiceResponse>>
        );
    }
}