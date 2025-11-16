using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public partial class Errors
{
    public class Worker
    {
        public static Error NotAllowedName => Error.Conflict(
            "Core.Worker.NotAllowedName.Title",
            "Core.Worker.NotAllowedName.Description");
        public static Error CannotChangeToSamePermission => Error.Conflict(
            "Core.Worker.CannotChangeToSamePermission.Title",
            "Core.Worker.CannotChangeToSamePermission.Description");
        public static Error NotFound => Error.NotFound(
            "Core.Worker.NotFound.Title",
            "Core.Worker.NotFound.Description");
        public static Error InvalidPermission => Error.Conflict(
            "Core.Worker.InvalidPermission.Title",
            "Core.Worker.InvalidPermission.Description");
        public static Error DoesNotBelongToStore => Error.Conflict(
                    "Core.Worker.DoesNotBelongToStore.Title",
                    "Core.Worker.DoesNotBelongToStore.Description");
    }
}