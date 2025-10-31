using BeatEcoprove.Domain.ProfileAggregator.Enumerators;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;

namespace BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;

public class Consumer : Profile
{
    private Consumer() { }

    private Consumer(
        DisplayName displayName,
        string firstName,
        string lastName,
        string biography,
        Phone phone,
        double xP,
        int sustainabilityPoints,
        int ecoScore,
        DateOnly bornDate,
        Gender gender)
        : base(displayName, firstName, lastName, biography, phone, xP, sustainabilityPoints, ecoScore, UserType.Consumer)
    {
        BornDate = bornDate;
        Gender = gender;
        Type = UserType.Consumer;
    }

    public DateOnly BornDate { get; private set; }
    public Gender Gender { get; private set; }

    public static Consumer Create(
        DisplayName displayName,
        string firstName,
        string lastName,
        string biography,
        Phone phone,
        DateOnly bornDate,
        Gender gender)
    {
        return new Consumer(
            displayName,
            firstName,
            lastName,
            biography,
            phone,
            0.0,
            0,
            0,
            bornDate,
            gender);
    }
}