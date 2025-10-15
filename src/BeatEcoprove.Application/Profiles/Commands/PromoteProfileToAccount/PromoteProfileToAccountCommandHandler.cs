using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Interfaces.Persistence;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;

using ErrorOr;

namespace BeatEcoprove.Application.Profiles.Commands.PromoteProfileToAccount;

internal sealed class PromoteProfileToAccountCommandHandler : ICommandHandler<PromoteProfileToAccountCommand, ErrorOr<Profile>>
{
    private readonly IProfileManager _profileManager;
    private readonly IUnitOfWork _unitOfWork;

    public PromoteProfileToAccountCommandHandler(
        IProfileManager profileManager,
        IUnitOfWork unitOfWork)
    {
        _profileManager = profileManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Profile>> Handle(PromoteProfileToAccountCommand request, CancellationToken cancellationToken)
    {
        var profileId = ProfileId.Create(request.ProfileId);
        var authId = AuthId.Create(request.AuthId);

        var profile = await _profileManager.GetProfileAsync(profileId, cancellationToken);

        if (profile.IsError)
        {
            return profile.Errors;
        }

        profile.Value.SetAuthId(authId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return profile.Value;
    }
}