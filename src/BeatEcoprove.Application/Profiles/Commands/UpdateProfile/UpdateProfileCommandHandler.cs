using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Helpers;
using BeatEcoprove.Application.Shared.Interfaces.Persistence;
using BeatEcoprove.Application.Shared.Interfaces.Providers;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Extensions;

using ErrorOr;

namespace BeatEcoprove.Application.Profiles.Commands.UpdateProfile;

internal sealed class UpdateProfileCommandHandler(
    IProfileManager profileManager,
    IUnitOfWork unitOfWork,
    IFileStorageProvider storageProvider)
    : ICommandHandler<UpdateProfileCommand, ErrorOr<Profile>>
{
    private readonly IFileStorageProvider _storageProvider = storageProvider;

    public async Task<ErrorOr<Profile>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var profileId = ProfileId.Create(request.ProfileId);
        var username = request.Input.DisplayName != null ? DisplayName.Create(request.Input.DisplayName) : (ErrorOr<DisplayName>?)null;
        var phone = request.Input.PhoneNumber is not null ? Phone.Create(request.Input.PhoneNumber) : (ErrorOr<Phone>?)null;

        ErrorOr<bool> validator = false;
        if (username != null)
        {
            validator = validator.AddValidate(username.Value);
        }

        if (phone != null)
        {
            validator = validator.AddValidate(phone.Value);
        }

        if (validator.IsError)
        {
            return validator.Errors;
        }

        var profile = await profileManager.GetProfileAsync(profileId, cancellationToken);

        if (profile.IsError)
        {
            return profile.Errors;
        }

        profile.Value.Update(username?.Value, phone?.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return profile.Value;
    }
}