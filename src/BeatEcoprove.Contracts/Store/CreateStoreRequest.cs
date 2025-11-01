namespace BeatEcoprove.Contracts.Store;

public record CreateStoreRequest
(
    string Name,
    string Country,
    string Locality,
    string Street,
    string ZipCode,
    int Port,
    double Lat,
    double Lon
);