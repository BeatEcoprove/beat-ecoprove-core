using BeatEcoprove.Domain.ProfileAggregator.Enumerators;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;

namespace BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;

public class Organization : Profile
{
    private Organization()
    {
    }

    private Organization(
        DisplayName displayName,
        string firstName,
        string lastName,
        string biography,
        Phone phone,
        double xP,
        int sustainabilityPoints,
        int ecoScore,
        Address address)
        : base(displayName, firstName, lastName, biography, phone, xP, sustainabilityPoints, ecoScore, UserType.Organization)
    {
        Address = address;
        TypeOption = TypeOption.Washer;
        Type = UserType.Organization;
    }

    public Address Address { get; private set; } = null!;
    public TypeOption TypeOption { get; private set; }

    public static Organization Create(
        DisplayName displayName,
        string firstName,
        string lastName,
        string biography,
        Phone phone,
        Address address)
    {
        return new Organization(
            displayName,
            firstName,
            lastName,
            biography,
            phone,
            0.0,
            0,
            0,
            address);
    }
}