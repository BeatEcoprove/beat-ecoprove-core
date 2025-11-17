using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Gaming;
using BeatEcoprove.Application.Shared.Interfaces.Persistence;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.ProfileAggregator.Enumerators;
using BeatEcoprove.Domain.ProfileAggregator.Events;
using BeatEcoprove.Domain.ProfileAggregator.Factories;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Broker;
using BeatEcoprove.Domain.Shared.Errors;

using ErrorOr;

using MassTransit;

namespace BeatEcoprove.Application.Profiles.Commands.CreateProfile;

internal sealed class CreateConsumerCommandHandler(
    IProfileFactory profileFactory,
    IProfileRepository profileRepository,
    IProfileCreateService profileCreateService,
    ITopicProducer<IAuthEvent> publishAuthEvents,
    IGamingService gamingService,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateConsumerCommand, ErrorOr<GamificationDTO>>
{
    public async Task<ErrorOr<GamificationDTO>> Handle(
        CreateConsumerCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await profileCreateService.ValidateConsumerDetails(request.Personal, cancellationToken);

        if (validationResult.IsError)
            return validationResult.Errors;

        (DisplayName displayName, Phone phone, Gender gender) = validationResult.Value;

        var profileId = ProfileId.Create(request.ProfileId);

        var authValidation = await profileCreateService.ValidateAuthEntry(profileId);

        if (authValidation.IsError)
            return authValidation.Errors;

        var authId = authValidation.Value;

        var foundProfile = await profileRepository.GetByIdAsync(profileId, cancellationToken);

        if (foundProfile is not null)
            return Errors.Profile.AlreadyExists;

        var profile = profileFactory.CreateProfile(
            new ProfileDetails(
                profileId,
                authId,
                displayName,
                request.Personal.FirstName,
                request.Personal.LastName,
                request.Personal.Bio,
                phone
            ),
            new ConsumerDetails(request.Personal.BirthDate, gender)
        );

        if (profile.IsError)
            return profile.Errors;

        await profileRepository.AddAsync(profile.Value, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await publishAuthEvents.Produce(
            new ProfileCreatedEvent(
                profile.Value.AuthId,
                profile.Value.Id,
                profile.Value.DisplayName,
                AuthRole.Client
            ),
            cancellationToken
        );

        var nextLevel = gamingService.GetLevelProgress(profile.Value);

        return new GamificationDTO(
            profile.Value,
            nextLevel
        );
    }
}