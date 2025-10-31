namespace BeatEcoprove.Application.Shared.Inputs;

public record AddressInfoInput(
    string Street,
    int Port,
    string Locality,
    string ZipCode
);