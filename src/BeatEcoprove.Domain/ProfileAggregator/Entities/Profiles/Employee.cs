using BeatEcoprove.Domain.ProfileAggregator.Enumerators;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;

namespace BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;

public class Employee : Profile
{
    private Employee() { }

    private Employee(
        DisplayName displayName,
        string firstName,
        string lastName,
        string biography,
        Phone phone,
        double xP,
        int sustainabilityPoints,
        int ecoScore,
        UserType type) : base(displayName, firstName, lastName, biography, phone, xP, sustainabilityPoints, ecoScore, type)
    {
        Type = UserType.Employee;
    }

    public static Employee Create(
         DisplayName displayName,
         string firstName,
         string lastName,
         string biography,
         Phone phone
    )
    {
        return new Employee(
            displayName,
            firstName,
            lastName,
           biography,
            phone,
            0D,
            0,
            0,
            UserType.Employee
        );
    }
}