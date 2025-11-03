using BeatEcoprove.Application.Cloths.Queries.Common.HistoryResult;
using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.ClosetAggregator.Entities;
using BeatEcoprove.Domain.ClosetAggregator.ValueObjects;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Errors;

using ErrorOr;

namespace BeatEcoprove.Application.Cloths.Queries.GetHistory;

internal sealed class GetHistoryQueryHandler(
    IProfileManager profileManager,
    IClosetService closetService,
    IClothRepository clothRepository,
    IMaintenanceServiceRepository maintenanceServiceRepository)
    : IQueryHandler<GetHistoryQuery, ErrorOr<List<HistoryResult>>>
{
    public async Task<ErrorOr<List<HistoryResult>>> Handle(GetHistoryQuery request, CancellationToken cancellationToken)
    {
        var profileId = ProfileId.Create(request.ProfileId);
        var clothId = ClothId.Create(request.ClothId);

        var profile = await profileManager.GetProfileAsync(profileId, cancellationToken);

        if (profile.IsError)
        {
            return profile.Errors;
        }

        var cloth = await closetService.GetCloth(profile.Value, clothId, cancellationToken);

        if (cloth.IsError)
        {
            return cloth.Errors;
        }

        var history = await clothRepository.GetHistoryOfActivities(clothId, cancellationToken);

        foreach (var activity in history)
        {

        }

        List<HistoryResult> historyActivities = history.Select(activity =>
        {
            ErrorOr<HistoryResult> task = activity switch
            {
                MaintenanceActivity maintenanceActivity => HandleMaintenaceActivity(maintenanceActivity, cancellationToken).GetAwaiter().GetResult(),
                DailyUseActivity dailyUseActivity => HandleDailyActivity(dailyUseActivity),
                _ => Errors.MaintenanceService.NotFound,
            };

            return task;
        })
        .Where(activity => !activity.IsError)
        .Select(activity => activity.Value)
        .ToList();

        return historyActivities;
    }

    private ErrorOr<HistoryResult> HandleDailyActivity(DailyUseActivity activity)
    {
        return new DailyHistoryResult(
           "Piece was used on",
            activity.EndAt ?? DateTimeOffset.UtcNow
        );
    }

    private async Task<ErrorOr<HistoryResult>> HandleMaintenaceActivity(MaintenanceActivity activity, CancellationToken cancellationToken = default)
    {
        var maintenaceId = MaintenanceServiceId.Create(activity.ServiceId);
        var actionId = MaintenanceActionId.Create(activity.ActionId);

        var maintenaceService = await maintenanceServiceRepository.GetByIdAsync(maintenaceId, cancellationToken);

        if (maintenaceService is null)
        {
            return Errors.MaintenanceService.NotFound;
        }

        var action = await maintenanceServiceRepository.GetActionByIdAsync(actionId, cancellationToken);

        if (action is null)
        {
            return Errors.MaintenanceService.NotFoundAction;
        }

        return new MaintenaceHistoryResult(
            maintenaceService.Title,
            action.Title,
            activity.EndAt ?? DateTimeOffset.UtcNow
        );


    }
}