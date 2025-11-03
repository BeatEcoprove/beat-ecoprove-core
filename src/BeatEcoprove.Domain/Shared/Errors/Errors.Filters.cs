using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public static partial class Errors
{
    public static class Filters
    {
        public static Error Category => Error.Validation(
            "Core.Filter.Category.Title",
            "Core.Filter.Category.Description");

        public static Error Size => Error.Validation(
            "Core.Filter.Size.Title",
            "Core.Filter.Size.Description");

        public static Error Order => Error.Validation(
            "Core.Filter..Title",
            "Core.Filter.Order.Description");
    }
}