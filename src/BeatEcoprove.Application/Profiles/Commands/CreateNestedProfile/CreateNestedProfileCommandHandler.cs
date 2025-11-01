using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Extensions;
using BeatEcoprove.Application.Shared.Helpers;
using BeatEcoprove.Application.Shared.Interfaces.Persistence;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Providers;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;
using BeatEcoprove.Domain.ProfileAggregator.Enumerators;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Errors;

using ErrorOr;

namespace BeatEcoprove.Application.Profiles.Commands.CreateNestedProfile;

internal sealed class CreateNestedProfileCommandHandler(
    IProfileManager profileManager,
    IProfileRepository profileRepository,
    IUnitOfWork unitOfWork,
    IFileStorageProvider fileStorageProvider)
    : ICommandHandler<CreateNestedProfileCommand, ErrorOr<Profile>>
{
    public async Task<ErrorOr<Profile>> Handle(CreateNestedProfileCommand request, CancellationToken cancellationToken)
    {
        // Validate request
        var userName = DisplayName.Create(request.UserName.Capitalize());
        if (!Enum.TryParse<Gender>(request.Gender, out var gender))
        {
            return Errors.User.InvalidUserGender;
        }

        if (userName.IsError)
        {
            return userName.Errors;
        }

        if (await profileRepository.ExistsUserByUserNameAsync(userName.Value, cancellationToken))
        {
            return Errors.User.UserNameAlreadyExists;
        }

        // Get Main Profile and check profile
        var profile = await profileManager.GetProfileAsync(request.AuthId, cancellationToken);

        if (profile.IsError)
        {
            return profile.Errors;
        }

        // Create Nested Profile
        var phone = Phone.Create(profile.Value.Phone.Code, profile.Value.Phone.Value);

        var nestedProfile = Consumer.Create(
            userName.Value,
            string.Empty,
            String.Empty,
            String.Empty,
            phone.Value,
            request.BornDate,
            gender
        );

        // Add Nested Profile to Main Profile
        nestedProfile.SetAuthId(profile.Value.AuthId);

        // Set avatar and store it on database
        var avatarUrl =
            await fileStorageProvider
                .UploadFileAsync(
                    Buckets.ProfileBucket,
                    ((Guid)nestedProfile.Id).ToString(),
                    request.AvatarPicture,
                    cancellationToken);

        nestedProfile.SetProfileAvatar(avatarUrl);

        // Save Nested Profile
        await profileRepository.AddAsync(nestedProfile, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return nestedProfile;
    }
}