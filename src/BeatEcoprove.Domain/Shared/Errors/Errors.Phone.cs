using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public static partial class Errors
{
    public class Phone
    {
        public static Error EmptyPhone => Error.Validation(
            "Core.Phone.EmptyPhone.Title",
            "Core.Phone.EmptyPhone.Description");

        public static Error InvalidPhone => Error.Validation(
            "Core.Phone.InvalidPhone.Title",
            "Core.Phone.InvalidPhone.Description");

        public static Error MustBeNineLegth => Error.Validation(
            "Core.Phone.MustBeNineLength.Title",
            "Core.Phone.MustBeNineLength.Description");
    }
}