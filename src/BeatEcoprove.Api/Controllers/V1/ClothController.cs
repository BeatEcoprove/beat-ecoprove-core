using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Cloths.Commands.CloseMaintenanceActivity;
using BeatEcoprove.Application.Cloths.Commands.PerformAction;
using BeatEcoprove.Application.Cloths.Queries.Common.HistoryResult;
using BeatEcoprove.Application.Cloths.Queries.GetAvailableServices;
using BeatEcoprove.Application.Cloths.Queries.GetClothMaintenanceStatus;
using BeatEcoprove.Application.Cloths.Queries.GetHistory;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Contracts.Closet.Cloth;
using BeatEcoprove.Contracts.Closet.Cloth.HistoryResponse;
using BeatEcoprove.Contracts.Services;

using Mapster;

using MapsterMapper;

using MediatR;

namespace BeatEcoprove.Api.Controllers.V1;

public class ClothController : ApiCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var cloth = CreateVersionedGroup(app, "profiles/closet/cloth/{clothId:guid}/services")
            .RequireAuthorization();

        cloth.MapGet(String.Empty, GetAvailableServices);
        cloth.MapPost("{serviceId:guid}/perform/{actionId:guid}", PerformService);
        cloth.MapPost("{maintenanceActivityId:guid}/finish", CloseAction);
        cloth.MapPatch("current", GetClothMaintenanceStatus);
        cloth.MapPatch("history", GetClothHistory);
    }

    private static async Task<IResult> GetAvailableServices(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid clothId,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
        {
            ProfileId = profileId,
            ClothId = clothId
        }.Adapt<GetAvailableServicesQuery>(),
            cancellationToken
        );

        return result.Match(
            response => Results.Ok(
                mapper.Map<List<MaintenanceServiceResponse>>(response)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> PerformService(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid serviceId,
        Guid clothId,
        Guid actionId,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
        {
            ProfileId = profileId,
            ClothId = clothId,
            ServiceId = serviceId,
            ActionId = actionId
        }.Adapt<PerformActionCommand>(), cancellationToken);

        return result.Match(
            response => Results.Ok(
                mapper.Map<ClothResponse>(response)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> CloseAction(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid clothId,
        Guid maintenanceActivityId,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
            CloseMaintenanceActivityCommand(
                profileId,
                clothId,
                maintenanceActivityId
            ), cancellationToken);

        return result.Match(
            response => Results.Ok(
                mapper.Map<ClothResponse>(response)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> GetClothMaintenanceStatus(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid clothId,
        Guid maintenanceActivityId,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
            GetClothMaintenanceStatusQuery(
                profileId,
                clothId
            ), cancellationToken);

        return result.Match(
            response => Results.Ok(
                mapper.Map<ClothMaintenanceStatusResponse>(response)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> GetClothHistory(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid clothId,
        Guid maintenanceActivityId,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
            GetHistoryQuery(
                profileId,
                clothId
            )
, cancellationToken);

        return result.Match(
            response => Results.Ok(
                ProxyResponse(mapper, response)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static dynamic ProxyResponse(
        IMapper mapper,
        List<HistoryResult> history)
    {
        return history
            .Select(historyResult =>
            {
                object response = historyResult switch
                {
                    DailyHistoryResult dailyHistory => mapper.Map<DailyHistoryResponse>(dailyHistory),
                    MaintenaceHistoryResult maintenaceHistory => mapper.Map<MaintenanceHistoryResponse>(maintenaceHistory),
                    _ => throw new ArgumentException("Unsupported history type"),
                };

                return response;
            }).ToList();
    }
}