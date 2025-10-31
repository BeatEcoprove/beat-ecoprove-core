using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Inputs;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;

using ErrorOr;

namespace BeatEcoprove.Application.Profiles.Commands.UpdateProfile;

public record UpdateProfileCommand
(
    Guid ProfileId,
    UpdateInput Input
) : ICommand<ErrorOr<Profile>>;