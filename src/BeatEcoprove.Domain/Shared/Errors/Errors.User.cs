using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public static partial class Errors
{
    public class User
    {
        public static Error ProfileDoesNotBelongToAuth => Error.Conflict(
            "Core.User.ProfileDoesNotBelongToAuth.Title",
            "Core.User.ProfileDoesNotBelongToAuth.Description");

        public static Error ProfileDoesNotExists => Error.Conflict(
            "Core.User.ProfileDoesNotExists.Title",
            "Core.User.ProfileDoesNotExists.Description");

        public static Error EmailAlreadyExists => Error.Conflict(
            "Core.User.EmailAlreadyExists.Title",
            "Core.User.EmailAlreadyExists.Description");

        public static Error UserNameAlreadyExists => Error.Conflict(
            "Core.User.UserNameAlreadyExists.Title",
            "Core.User.UserNameAlreadyExists.Description");

        public static Error EmailDoesNotExists => Error.Conflict(
            "Core.User.EmailDoesNotExists.Title",
            "Core.User.EmailDoesNotExists.Description");

        public static Error BadCredentials => Error.Conflict(
            "Core.User.BadCredentials.Title",
            "Core.User.BadCredentials.Description");

        public static Error InvalidUserGender => Error.Conflict(
            "Core.User.InvalidUserGender.Title",
            "Core.User.InvalidUserGender.Description");
    }
}