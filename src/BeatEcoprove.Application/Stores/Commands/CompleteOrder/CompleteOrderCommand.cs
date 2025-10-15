using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.StoreAggregator.Daos;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Commands.CompleteOrder;

public record CompleteOrderCommand
(
    Guid ProfileId,
    Guid StoreId,
    Guid OrderId,
    Guid OwnerId
) : ICommand<ErrorOr<OrderDAO>>;