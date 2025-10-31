namespace BeatEcoprove.Application.Shared.Inputs;

public record UpdateInput(
    string? FirstName,
    string? LastName,
    string? DisplayName,
    string? Bio,
    string? PhoneNumber
);