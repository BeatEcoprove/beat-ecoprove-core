using BeatEcoprove.Application.Shared.Extensions;
using BeatEcoprove.Application.Shared.Inputs;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.ProfileAggregator.Enumerators;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Errors;

using ErrorOr;

namespace BeatEcoprove.Infrastructure.Services;

public class ProfileCreateService(
    IProfileRepository profileRepository,
    IKeyValueRepository<string> fetchUserCreated) : IProfileCreateService 
{
    private async Task<AuthId?> GetCurrentProfile(ProfileId profileId, CancellationToken cancellationToken)
       => (await profileRepository.GetByIdAsync(profileId, cancellationToken))?.AuthId;
    
    public async Task<ErrorOr<AuthId>> ValidateAuthEntry(ProfileId profileId)
    {
        var userCreatedKey = new UserCreatedKey(profileId);
        var foundUserEntry = await fetchUserCreated.GetAndDeleteAsync(userCreatedKey);

        if (foundUserEntry == null)
            return Errors.Auth.InvalidAuth;
        
        var entry = foundUserEntry.Split(":");
        
        if (entry.Length < 2 || entry[1] != AuthRole.Client || !Guid.TryParse(entry[0], out var authIdGuid))
            return Errors.Auth.InvalidAuth;

        return AuthId.Create(authIdGuid);
    }
    
    public async Task<ErrorOr<(DisplayName, Phone, Gender)>> ValidateConsumerDetails(
        PersonalInfoInput request,
        CancellationToken cancellationToken)
    {
        var displayName = DisplayName.Create(request.DisplayName.Capitalize());
        
        if (displayName.IsError)
            return displayName.Errors;

        if (await profileRepository.ExistsUserByUserNameAsync(displayName.Value, cancellationToken))
            return Errors.User.UserNameAlreadyExists;

        var phone = Phone.Create(request.PhoneNumber);
        if (phone.IsError)
            return phone.Errors;

        if (!Enum.TryParse<Gender>(request.Gender, ignoreCase: true, out var gender))
            return Errors.User.InvalidUserGender;

        return (displayName.Value, phone.Value, gender);
    }

    public async Task<ErrorOr<(DisplayName, Phone, Address)>> ValidateOrganizationDetails(
        OrganizationInfoInput request, 
        CancellationToken cancellationToken)
    {
        var displayName = DisplayName.Create(request.DisplayName.Capitalize());
        
        if (displayName.IsError)
            return displayName.Errors;

        if (await profileRepository.ExistsUserByUserNameAsync(displayName.Value, cancellationToken))
            return Errors.User.UserNameAlreadyExists;

        var phone = Phone.Create(request.PhoneNumber);
        
        if (phone.IsError)
            return phone.Errors;

        var postalCode = PostalCode.Create(request.Address.ZipCode);
        
        if (postalCode.IsError)
            return postalCode.Errors;

        var address = Address.Create(
            request.Address.Street,
            request.Address.Port,
            request.Address.Locality,
            postalCode.Value);
        
        if (address.IsError)
            return address.Errors;
       
        return (displayName.Value, phone.Value, address.Value);
    }
}