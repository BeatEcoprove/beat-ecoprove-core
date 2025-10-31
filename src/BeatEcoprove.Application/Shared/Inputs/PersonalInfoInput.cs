namespace BeatEcoprove.Application.Shared.Inputs;

public record PersonalInfoInput(
    string FirstName,
    string LastName,
    string DisplayName,
    DateOnly BirthDate,
    string Gender,
    string Bio,
    string PhoneNumber
);