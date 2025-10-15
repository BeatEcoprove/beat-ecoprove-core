using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.StoreAggregator;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Commands.AddStore;

public record AddStoreCommand
(
    Guid ProfileId,
    string Name,
    string Country,
    string Locality,
    string Street,
    string PostalCode,
    int Port,
    double Lat,
    double Lon,
    Stream Picture
) : ICommand<ErrorOr<Store>>;