using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public partial class Errors
{
    public class Brand
    {
        public static Error MustProvideBrandName => Error.Conflict(
            "Core.Brand.MustProvideBrandName.Title",
            "Core.Brand.MustProvideBrandName.Description");

        public static Error MustProvideBrandAvatar => Error.Conflict(
            "Core.Brand.MustProvideBrandAvatar.Title",
            "Core.Brand.MustProvideBrandAvatar.Description");

        public static Error ThereIsNoBrandName => Error.Conflict(
            "Core.Brand.ThereIsNoBrandName.Title",
            "Core.Brand.ThereIsNoBrandName.Description");
    }
}