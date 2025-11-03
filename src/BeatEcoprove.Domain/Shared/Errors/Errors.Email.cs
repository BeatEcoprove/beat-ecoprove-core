using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public static partial class Errors
{
    public class Email
    {
        public static Error EmptyEmail => Error.Validation(
            "Core.Email.EmptyEmail.Title",
            "Core.Email.EmptyEmail.Description");

        public static Error InvalidEmail => Error.Validation(
            "Core.Email.InvalidEmail.Title",
            "Core.Email.InvalidEmail.Description");
    }
}