using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public partial class Errors
{
    public class Advertisement
    {
        public static Error CannotPerformThis => Error.Conflict(
            "Core.Advertisement.CannotPerformThis.Title",
            "Core.Advertisement.CannotPerformThis.Description");
        public static Error DontHaveEnoughPoint => Error.Conflict(
                    "Core.Advertisement.DontHaveEnoughPoint.Title",
                    "Core.Advertisement.DontHaveEnoughPoint.Description");
        public static Error VoucherQuantityBelow0 => Error.Conflict(
            "Core.Advertisement.VoucherQuantityBelow0.Title",
            "Core.Advertisement.VoucherQuantityBelow0.Description");

        public static Error DateMustBeValid => Error.Conflict(
            "Core.Advertisement.DateMustBeValid.Title",
            "Core.Advertisement.DateMustBeValid.Description");

        public static Error NotFound => Error.NotFound(
            "Core.Advertisement.NotFound.Title",
            "Core.Advertisement.NotFound.Description");
    }
}