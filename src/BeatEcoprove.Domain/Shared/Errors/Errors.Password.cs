using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public static partial class Errors
{
    public class Password
    {
        public static Error EmptyPassword => Error.Validation(
            "Core.Password.EmptyPassword.Title",
            "Core.Password.EmptyPassword.Description");

        public static Error MustBeBetween6And16 => Error.Validation(
            "Core.Password.MustBeBetween6And16.Title",
            "Core.Password.MustBeBetween6And16.Description");

        public static Error MustContainAtLeastOnNumber => Error.Validation(
            "Core.Password.MustContainAtLeastOnNumber.Title",
            "Core.Password.MustContainAtLeastOnNumber.Description");

        public static Error MustContainAtLeastOnCaptialLetter => Error.Validation(
            "Core.Password.MustContainAtLeastOnCaptialLetter.Title",
            "Core.Password.MustContainAtLeastOnCaptialLetter.Description");

        public static Error MustContainAtLeastLetter => Error.Validation(
            "Core.Password.MustContainAtLeastLetter.Title",
            "Core.Password.MustContainAtLeastLetter.Description");
    }
}