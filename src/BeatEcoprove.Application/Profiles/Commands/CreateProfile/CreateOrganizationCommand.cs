using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Inputs;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;

using ErrorOr;

namespace BeatEcoprove.Application.Profiles.Commands.CreateProfile;

public record CreateOrganizationCommand
(
    Guid ProfileId,
    OrganizationInfoInput Detail
) : ICommand<ErrorOr<Profile>>;
