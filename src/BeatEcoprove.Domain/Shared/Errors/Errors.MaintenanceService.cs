using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public partial class Errors
{
    public class MaintenanceService
    {
        public static Error NotFound => Error.Conflict(
            "Core.MaintenanceService.NotFound.Title",
            "Core.MaintenanceService.NotFound.Description");

        public static Error NotFoundAction => Error.Conflict(
            "Core.MaintenanceService.ActionNotFound.Title",
            "Core.MaintenanceService.ActionNotFound.Description");
    }
}