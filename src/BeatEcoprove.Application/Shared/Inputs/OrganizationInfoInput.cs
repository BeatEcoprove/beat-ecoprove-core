namespace BeatEcoprove.Application.Shared.Inputs;

public record OrganizationInfoInput(
    string FirstName,
    string LastName,
    string DisplayName,
    string Bio,
    string PhoneNumber,
    AddressInfoInput Address
);