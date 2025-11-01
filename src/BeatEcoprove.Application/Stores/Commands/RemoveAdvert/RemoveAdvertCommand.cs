using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.AdvertisementAggregator;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Commands.RemoveAdvert;

public record RemoveAdvertCommand
(
    Guid ProfileId,
    Guid AdvertId
) : ICommand<ErrorOr<Advertisement>>;