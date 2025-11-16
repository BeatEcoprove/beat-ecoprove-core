using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public static partial class Errors
{
    public class PostalCode
    {
        public static Error EmptyPostalCode => Error.Validation(
            "Core.PostalCode.EmptyPostalCode.Title",
            "Core.PostalCode.EmptyPostalCode.Description");

        public static Error InvalidPostalCode => Error.Validation(
            "Core.PostalCode.InvalidPostalCode.Title",
            "Core.PostalCode.InvalidPostalCode.Description");
    }
}