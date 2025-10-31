using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;
using BeatEcoprove.Domain.Shared.Errors;

using ErrorOr;

namespace BeatEcoprove.Domain.ProfileAggregator.Factories;

internal sealed class ProfileFactory : IProfileFactory
{
    public ErrorOr<Profile> CreateProfile(
        ProfileDetails details, 
        ProfileSpecificDetails profileSpecific)
    {
        var profile = profileSpecific switch
        {
            ConsumerDetails consumer => CreateConsumer(details, consumer),
            OrganizationDetails org => CreateOrganization(details, org),
            _ => Errors.Profile.NotFound,
        };

        profile.Value.SetProfileId(details.Id);
        profile.Value.SetAuthId(details.AuthId);
        profile.Value.SetProfileAvatar($"https://robohash.org/{profile.Value.Id.Value.ToString()}");
        
        return profile;
    }

    private static ErrorOr<Profile> CreateConsumer(
        ProfileDetails details, 
        ConsumerDetails consumerDetails)
    {
        return Consumer.Create(
            details.DisplayName,
            details.FirstName,
            details.LastName,
            details.Biography,
            details.Phone,
            consumerDetails.BornDate,
            consumerDetails.Gender
        );
    }
    
    private static ErrorOr<Profile> CreateOrganization(
        ProfileDetails details, 
        OrganizationDetails orgDetails)
    {
        return Organization.Create(
            details.DisplayName,
            details.FirstName,
            details.LastName,
            details.Biography,
            details.Phone,
            orgDetails.Address
        );
    }
}