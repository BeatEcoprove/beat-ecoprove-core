using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Interfaces.Persistence;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.ClosetAggregator.Entities;
using BeatEcoprove.Domain.ClosetAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Errors;

using ErrorOr;

namespace BeatEcoprove.Application.Closet.Commands.RegisterClothUsage;

internal sealed class RegisterClothUsageCommandHandler(
    IProfileManager profileManager,
    IClosetService closetService,
    IActivityRepository activityRepository,
    IUnitOfWork unitOfWork,
    IClothRepository clothRepository)
    : ICommandHandler<RegisterClothUsageCommand, ErrorOr<DailyUseActivity>>
{
    public async Task<ErrorOr<DailyUseActivity>> Handle(RegisterClothUsageCommand request, CancellationToken cancellationToken)
    {
        var clothId = ClothId.Create(request.ClothId);

        var profile = await profileManager.GetProfileAsync(request.ProfileId, cancellationToken);

        if (profile.IsError)
        {
            return profile.Errors;
        }

        var cloth = await closetService.GetCloth(profile.Value, clothId, cancellationToken);

        if (cloth.IsError)
        {
            return cloth.Errors;
        }

        var services = await clothRepository.GetAvailableMaintenanceServices(clothId, cancellationToken);

        if (services.Count == 0)
        {
            return Errors.Cloth.CannotUseClothBecauseIsOnMaintenance;
        }

        var activity = cloth.Value.UseCloth(profile.Value);

        if (activity.IsError)
        {
            return activity.Errors;
        }

        await activityRepository.AddAsync(activity.Value, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return activity;
    }
}