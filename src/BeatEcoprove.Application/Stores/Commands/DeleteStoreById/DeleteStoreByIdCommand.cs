using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.StoreAggregator;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Commands.DeleteStoreById;

public record DeleteStoreByIdCommand
(
    Guid ProfileId,
    Guid StoreId
) : ICommand<ErrorOr<Store>>;