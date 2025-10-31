using BeatEcoprove.Domain.ProfileAggregator.Enumerators;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;

namespace BeatEcoprove.Domain.ProfileAggregator.Factories;

public record ProfileDetails(
    ProfileId Id,
    AuthId AuthId,
    DisplayName DisplayName,
    string FirstName,
    string LastName,
    string Biography,
    Phone Phone
);

public abstract record ProfileSpecificDetails();

public record ConsumerDetails(
    DateOnly BornDate,
    Gender Gender
) : ProfileSpecificDetails();

public record OrganizationDetails(
    Address Address
) : ProfileSpecificDetails();