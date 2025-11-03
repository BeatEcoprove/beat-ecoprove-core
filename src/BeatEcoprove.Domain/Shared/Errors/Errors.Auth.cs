using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public static partial class Errors
{
    public class Auth
    {
        public static Error FailedRemovingSubProfiles => Error.Conflict(
            "Core.Auth.FailedRemovingSubProfiles.Title",
            "Core.Auth.FailedRemovingSubProfiles.Description");

        public static Error FailedRemovingAuthKey => Error.Conflict(
            "Core.Auth.FailedRemovingAuthKey.Title",
            "Core.Auth.FailedRemovingAuthKey.Description");

        public static Error InvalidAuth => Error.Conflict(
            "Core.Auth.InvalidAuth.Title",
            "Core.Auth.InvalidAuth.Description");
    }
}