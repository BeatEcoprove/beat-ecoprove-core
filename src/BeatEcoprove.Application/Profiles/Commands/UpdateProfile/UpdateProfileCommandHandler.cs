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

internal sealed class UpdateProfileCommandHandler : ICommandHandler<UpdateProfileCommand, ErrorOr<Profile>>
{
    private readonly IProfileManager _profileManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageProvider _storageProvider;

    public UpdateProfileCommandHandler(
        IProfileManager profileManager,
        IUnitOfWork unitOfWork,
        IFileStorageProvider storageProvider)
    {
        _profileManager = profileManager;
        _unitOfWork = unitOfWork;
        _storageProvider = storageProvider;
    }

    public async Task<ErrorOr<Profile>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var profileId = ProfileId.Create(request.ProfileId);
        var username = request.Username != null ? UserName.Create(request.Username) : (ErrorOr<UserName>?)null;
        var phone = request is { Phone: not null, PhoneCountryCode: not null } ? Phone.Create(request.PhoneCountryCode, request.Phone) : (ErrorOr<Phone>?)null;

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

        var profile = await _profileManager.GetProfileAsync(profileId, cancellationToken);

        if (profile.IsError)
        {
            return profile.Errors;
        }

        if (request.AvatarPicture != Stream.Null)
        {
            var avatarUrl =
                await _storageProvider
                    .UploadFileAsync(
                        Buckets.ProfileBucket,
                        ((Guid)profile.Value.Id).ToString(),
                        request.AvatarPicture,
                        cancellationToken);

            profile.Value.SetProfileAvatar(avatarUrl);
        }

        profile.Value.Update(username?.Value, phone?.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return profile.Value;
    }
}