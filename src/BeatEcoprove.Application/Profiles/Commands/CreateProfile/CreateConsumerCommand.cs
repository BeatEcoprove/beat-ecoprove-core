using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Inputs;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;

using ErrorOr;

namespace BeatEcoprove.Application.Profiles.Commands.CreateProfile;

public record CreateConsumerCommand
(
    Guid ProfileId,
    PersonalInfoInput Personal
) : ICommand<ErrorOr<Profile>>;