using BeatEcoprove.Application.Closet.Common;
using BeatEcoprove.Application.Shared;

using ErrorOr;

namespace BeatEcoprove.Application.Cloths.Commands.CloseMaintenanceActivity;

public record CloseMaintenanceActivityCommand
(
    Guid ProfileId,
    Guid ClothId,
    Guid MaintenanceActivityId
) : ICommand<ErrorOr<ClothResult>>;