using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.StoreAggregator.Daos;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Commands.PostRating;

public record PostRatingCommand
(
    Guid ProfileId,
    Guid StoreId,
    double Rating
) : ICommand<ErrorOr<RatingDao>>;