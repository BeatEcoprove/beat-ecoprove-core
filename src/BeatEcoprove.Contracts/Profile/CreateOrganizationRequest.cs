namespace BeatEcoprove.Contracts.Profile;

public record CreateOrganizationRequest(
    Guid ProfileId,
    string DisplayName,
    string FirstName,
    string LastName,
    string Biography,
    string PhoneNumber,
    string Street,
    int Port,
    string Locality,
    string ZipCode
);