using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Api.Mappers;
using BeatEcoprove.Application.Stores.Commands.CompleteOrder;
using BeatEcoprove.Application.Stores.Commands.RegisterOrder;
using BeatEcoprove.Application.Stores.Queries.GetOrderById;
using BeatEcoprove.Application.Stores.Queries.GetOrders;
using BeatEcoprove.Contracts.Store;

using MapsterMapper;

using MediatR;

namespace BeatEcoprove.Api.Controllers.V1;

public class OrderController : ApiCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var order = CreateVersionedGroup(app, "orders")
            .WithTags("Orders")
            .RequireAuthorization();

        order.MapGet("{orderId:guid}/stores/{storeId:guid}", GetOrdersById)
            .RequireScopes("order:view");

        order.MapGet(String.Empty, GetOrders)
            .RequireScopes("order:view");

        order.MapPost(String.Empty, CreateOrder)
            .RequireScopes("order:create");

        order.MapPatch("{orderId:guid}", CompleteOrder)
            .RequireScopes("order:update");
    }

    private static async Task<IResult> GetOrdersById(
        ISender sender,
        IMapper mapper,
        HttpContext context,
        Guid orderId,
        Guid storeId,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
                GetOrderByIdQuery(
                    profileId,
                    orderId,
                    storeId
                ), cancellationToken
        );

        return result.Match(
            response => Results.Ok(
                mapper.Map<OrderResponse>(result)),
            errors => errors.ToProblemDetails(context)
        );
    }

    private static async Task<IResult> GetOrders(
        ISender sender,
        IMapper mapper,
        HttpContext context,
        string storeIds,
        string? search,
        string? services,
        string? color,
        string? brand,
        int page,
        int pageSize,
        bool isDone,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
                GetOrdersQuery(
                    profileId,
                    storeIds,
                    search,
                    services,
                    color,
                    brand,
                    isDone,
                    page,
                    pageSize
                ), cancellationToken
        );

        return result.Match(
            response => Results.Ok(
                response.Select(StoreMappingConfiguration.CreateOrderResponse)),
            errors => errors.ToProblemDetails(context)
        );
    }

    private static async Task<IResult> CreateOrder(
        ISender sender,
        IMapper mapper,
        HttpContext context,
        CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
                RegisterOrderCommand(
                    profileId,
                    request.StoreId,
                    request.OwnerId,
                    request.ClothId
                ), cancellationToken
        );

        return result.Match(
            response => Results.Ok(
                mapper.Map<OrderResponse>(result)),
            errors => errors.ToProblemDetails(context)
        );
    }

    private static async Task<IResult> CompleteOrder(
        ISender sender,
        IMapper mapper,
        HttpContext context,
        Guid orderId,
        CompleteOrderRequest request,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
                CompleteOrderCommand(
                    profileId,
                    request.StoreId,
                    orderId,
                    request.OwnerId
                ), cancellationToken
        );

        return result.Match(
            response => Results.Ok(
                mapper.Map<OrderResponse>(result)),
            errors => errors.ToProblemDetails(context)
        );
    }
}