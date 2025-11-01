using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.StoreAggregator.Daos;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Queries.GetOrderById;

public record GetOrderByIdQuery
(
    Guid ProfileId,
    Guid OrderId,
    Guid StoreId
) : IQuery<ErrorOr<OrderDAO>>;