namespace BeatEcoprove.Contracts.Profile;

public record CreateClientProfileRequest(
    Guid ProfileId,
    string DisplayName,
    string FirstName,
    string LastName,
    string Biography,
    DateOnly BirthDate,
    string Gender,
    string PhoneNumber
);