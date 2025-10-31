using BeatEcoprove.Contracts.Common;

using Microsoft.AspNetCore.Http;

namespace BeatEcoprove.Contracts.Profile;

public record UpdateProfileRequest
(
    string? DisplayName,
    string? FirstName,
    string? LastName,
    string? Biography,
    DateOnly? BirthDate,
    string? Gender,
    string? PhoneNumber
);