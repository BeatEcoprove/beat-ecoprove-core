using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public static partial class Errors
{
    public class Color
    {
        public static Error MustProvideColor => Error.Validation(
            "Core.Color.MustProvideColor.Title",
            "Core.Color.MustProvideColor.Description");

        public static Error BadHexValue => Error.Validation(
            "Core.Color.BadHexValue.Title",
            "Core.Color.BadHexValue.Description");
    }
}