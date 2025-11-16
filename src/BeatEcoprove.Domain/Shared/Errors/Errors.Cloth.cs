using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public partial class Errors
{
    public class Cloth
    {
        public static Error InvalidClothName => Error.Validation(
            "Core.Cloth.InvalidClothName.Title",
            "Core.Cloth.InvalidClothName.Description");

        public static Error ClothNotFound => Error.Conflict(
            "Core.Cloth.ClothNotFound.Title",
            "Core.Cloth.ClothNotFound.Description");

        public static Error ClothIdBlocked => Error.Conflict(
            "Core.Cloth.ClothIdBlocked.Title",
            "Core.Cloth.ClothIdBlocked.Description");

        public static Error MaintenanceActivityNotFound => Error.Conflict(
            "Core.Cloth.MaintenanceActivityNotFound.Title",
            "Core.Cloth.MaintenanceActivityNotFound.Description");

        public static Error InvalidClothType => Error.Validation(
            "Core.Cloth.InvalidClothType.Title",
            "Core.Cloth.InvalidClothType.Description");

        public static Error InvalidClothSize => Error.Validation(
            "Core.Cloth.InvalidClothSize.Title",
            "Core.Cloth.InvalidClothSize.Description");

        public static Error CannotUseCloth => Error.Conflict(
            "Core.Cloth.CannotUseCloth.Title",
            "Core.Cloth.CannotUseCloth.Description");

        public static Error CannotUseClothBecauseIsOnMaintenance => Error.Conflict(
            "Core.Cloth.CannotUseClothOnMaintenance.Title",
            "Core.Cloth.CannotUseClothOnMaintenance.Description");

        public static Error IsBeingMaintain => Error.Conflict(
            "Core.Cloth.IsBeingMaintain.Title",
            "Core.Cloth.IsBeingMaintain.Description");

        public static Error CannotDisposeCloth => Error.Conflict(
            "Core.Cloth.CannotDisposeCloth.Title",
            "Core.Cloth.CannotDisposeCloth.Description");

        public static Error CannotFinishMaintenanceActivity => Error.Conflict(
            "Core.Cloth.CannotFinishMaintenanceActivity.Title",
            "Core.Cloth.CannotFinishMaintenanceActivity.Description");

        public static Error CannotAccessBucket => Error.Conflict(
            "Core.Cloth.CannotAccessBucket.Title",
            "Core.Cloth.CannotAccessBucket.Description");
    }
}