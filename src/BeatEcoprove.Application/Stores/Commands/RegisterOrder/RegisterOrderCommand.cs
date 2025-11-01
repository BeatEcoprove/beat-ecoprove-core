using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.StoreAggregator.Daos;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Commands.RegisterOrder;

public record RegisterOrderCommand
(

    Guid ProfileId,
    Guid StoreId,
    Guid OwnerId,
    Guid ClothId
) : ICommand<ErrorOr<OrderDAO>>;