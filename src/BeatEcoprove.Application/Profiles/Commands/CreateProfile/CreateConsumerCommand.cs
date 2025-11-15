using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Gaming;
using BeatEcoprove.Application.Shared.Inputs;

using ErrorOr;

namespace BeatEcoprove.Application.Profiles.Commands.CreateProfile;

public record CreateConsumerCommand
(
    Guid ProfileId,
    PersonalInfoInput Personal
) : ICommand<ErrorOr<GamificationDTO>>;