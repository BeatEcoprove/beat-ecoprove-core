using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Application.Stores.Commands.AddWorker;
using BeatEcoprove.Application.Stores.Commands.DeleteStoreById;
using BeatEcoprove.Application.Stores.Commands.ElevatePermissionOnWorker;
using BeatEcoprove.Application.Stores.Queries.GetWorkerById;
using BeatEcoprove.Application.Stores.Queries.GetWorkers;
using BeatEcoprove.Contracts.Store;

using MapsterMapper;

using MediatR;

namespace BeatEcoprove.Api.Controllers.V1;

public class WorkerController : ApiCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var worker = CreateVersionedGroup(app, "stores/{storeId}/workers")
            .RequireAuthorization();

        worker.MapGet(string.Empty, GetWorkers);
        worker.MapGet("{workerId:guid}", GetWorkerById);
        worker.MapPost(String.Empty, CreateWorker);
        worker.MapDelete("{workerId:guid}", DeleteWorker);
        worker.MapPatch("{workerId:guid}/switch", ChangePermission);
    }

    private static async Task<IResult> GetWorkers(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid storeId,
        string? search,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
                GetWorkersQuery(
                    profileId,
                    storeId,
                    search,
                    page,
                    pageSize
                ), cancellationToken
        );

        return result.Match(
            profile => Results.Ok(
                mapper.Map<List<WorkerResponse>>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> GetWorkerById(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid storeId,
        Guid workerId,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
                GetWorkerByIdQuery(
                    profileId,
                    storeId,
                    workerId
                ), cancellationToken
        );

        return result.Match(
            profile => Results.Ok(
                mapper.Map<WorkerResponse>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }


    private static async Task<IResult> CreateWorker(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid storeId,
        CreateWorkerRequest request,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
                AddWorkerCommand(
                    profileId,
                    storeId,
                    request.Name,
                    request.Email,
                    request.Permission
                ), cancellationToken
        );

        return result.Match(
            profile => Results.Ok(
                mapper.Map<WorkerResponse>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> DeleteWorker(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid storeId,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
                DeleteStoreByIdCommand(
                    profileId,
                    storeId
                ), cancellationToken
        );

        return result.Match(
            profile => Results.Ok(
                mapper.Map<WorkerResponse>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> ChangePermission(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid storeId,
        Guid workerId,
        ElevateWorkerPermissionRequest request,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
                ElevatePermissionOnWorkerCommand(
                    profileId,
                    storeId,
                    workerId,
                    request.Permission
                ), cancellationToken
        );

        return result.Match(
            profile => Results.Ok(
                mapper.Map<WorkerResponse>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }
}