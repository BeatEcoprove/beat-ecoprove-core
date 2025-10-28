using Asp.Versioning;

using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Application.Stores.Commands.AddWorker;
using BeatEcoprove.Application.Stores.Commands.ElevatePermissionOnWorker;
using BeatEcoprove.Application.Stores.Commands.RemoveWorker;
using BeatEcoprove.Application.Stores.Queries.GetWorkerById;
using BeatEcoprove.Application.Stores.Queries.GetWorkers;
using BeatEcoprove.Contracts.Store;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeatEcoprove.Api.Controllers;

[ApiVersion(1)]
[Authorize]
[AuthorizationRole("organization", "employee")]
[Route("v{version:apiVersion}/stores/{storeId:guid}/workers")]
public class WorkerController(
    ILanguageCulture localizer,
    ISender sender,
    IMapper mapper)
    : ApiController(localizer)
{
    [HttpGet]
    public async Task<ActionResult<List<WorkerResponse>>> GetWorkers(
        [FromQuery] Guid profileId,
        [FromRoute] Guid storeId,
        [FromQuery] string? search,
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var authId = HttpContext.User.GetUserId();
                        
        var getWorkerById = await sender.Send(new
            GetWorkersQuery(
               profileId, 
               storeId, 
               search,
               page,
               pageSize
            ), cancellationToken
        );

        return getWorkerById.Match(
            result => Ok(mapper.Map<List<WorkerResponse>>(result)),
            Problem<List<WorkerResponse>>
        );
    }
    
    [HttpGet("{workerId:guid}")]
    public async Task<ActionResult<WorkerResponse>> GetWorkerById(
        [FromQuery] Guid profileId,
        [FromRoute] Guid storeId,
        [FromRoute] Guid workerId,
        CancellationToken cancellationToken = default
    ) {
        var authId = HttpContext.User.GetUserId();
                
        var getWorkerById = await sender.Send(new
            GetWorkerByIdQuery(
               profileId, 
               storeId, 
               workerId
            ), cancellationToken
        );

        return getWorkerById.Match(
            result => Ok(mapper.Map<WorkerResponse>(result)),
            Problem<WorkerResponse>
        );
    }
    
    [HttpPost]
    public async Task<ActionResult<WorkerResponse>> RegisterWorker(
        [FromQuery] Guid profileId,
        [FromRoute] Guid storeId,
        CreateWorkerRequest request,
        CancellationToken cancellationToken = default
    ) {
        var authId = HttpContext.User.GetUserId();
        
        var registerOrderResult = await sender.Send(new
            AddWorkerCommand(
                profileId,
                storeId,
                request.Name,
                request.Email,
                request.Permission
            ), cancellationToken
        );
        
        return registerOrderResult.Match(
            result => Ok(mapper.Map<WorkerResponse>(result)),
            Problem<WorkerResponse>
        );
    }

    [HttpDelete("{workerId:guid}")]
    public async Task<ActionResult<WorkerResponse>> RemoveWorker(
        [FromQuery] Guid profileId,
        [FromRoute] Guid storeId,
        [FromRoute] Guid workerId,
        CancellationToken cancellationToken = default)
    {
        var authId = HttpContext.User.GetUserId();
        
        var registerOrderResult = await sender.Send(new
             RemoveWorkerCommand(
                profileId,
                storeId,
                workerId
            ), cancellationToken
        );
        
        return registerOrderResult.Match(
            result => Ok(mapper.Map<WorkerResponse>(result)),
            Problem<WorkerResponse>
        );
    }

    [HttpPatch("{workerId:guid}/switch")]
    public async Task<ActionResult<WorkerResponse>> ChangePermission(
        [FromQuery] Guid profileId,
        [FromRoute] Guid storeId,
        [FromRoute] Guid workerId,
        ElevateWorkerPermissionRequest request,
        CancellationToken cancellationToken = default
    ) {
        var authId = HttpContext.User.GetUserId();
        
        var registerOrderResult = await sender.Send(new
             ElevatePermissionOnWorkerCommand(
                 profileId,
                 storeId,
                 workerId,
                 request.Permission
            ), cancellationToken
        );
        
        return registerOrderResult.Match(
            result => Ok(mapper.Map<WorkerResponse>(result)),
            Problem<WorkerResponse>
        );
    }
}