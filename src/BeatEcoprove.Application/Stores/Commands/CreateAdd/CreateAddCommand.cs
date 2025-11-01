using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.AdvertisementAggregator;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Commands.CreateAdd;

public record CreateAddCommand
(
    Guid ProfileId,
    Guid StoreId,
    string Title,
    string Description,
    DateOnly BeginAt,
    DateOnly EndAt,
    string Type,
    int Quantity = 0
) : ICommand<ErrorOr<Advertisement>>;