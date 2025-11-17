using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Interfaces.Persistence;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;

using ErrorOr;

namespace BeatEcoprove.Application.Profiles.Commands.DeleteNestedProfile;

internal sealed class DeleteNestedProfileCommandHandler(
    IProfileManager profileManager,
    IProfileRepository profileRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteNestedProfileCommand, ErrorOr<Profile>>
{
    public async Task<ErrorOr<Profile>> Handle(DeleteNestedProfileCommand request, CancellationToken cancellationToken)
    {
        var profileId = ProfileId.Create(request.ProfileId);

        var profile = await profileManager.GetProfileAsync(profileId, cancellationToken);

        if (profile.IsError)
        {
            return profile.Errors;
        }

        // delete the profile and save changes
        await profileRepository.DeleteAsync(profile.Value.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return profile;
    }
}