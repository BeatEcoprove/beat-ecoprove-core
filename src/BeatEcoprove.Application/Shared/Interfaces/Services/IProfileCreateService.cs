using BeatEcoprove.Application.Shared.Inputs;
using BeatEcoprove.Domain.ProfileAggregator.Enumerators;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;

using ErrorOr;

namespace BeatEcoprove.Application.Shared.Interfaces.Services;

public interface IProfileCreateService
{
    public Task<ErrorOr<AuthId>> ValidateAuthEntry(ProfileId profileId);

    public Task<ErrorOr<(DisplayName, Phone, Gender)>> ValidateConsumerDetails(
        PersonalInfoInput request,
        CancellationToken cancellationToken);

    public Task<ErrorOr<(DisplayName, Phone, Address)>> ValidateOrganizationDetails(
        OrganizationInfoInput request,
        CancellationToken cancellationToken);
}