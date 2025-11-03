using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public partial class Errors
{
    public class Username
    {
        public static Error InvalidUsername => Error.Validation(
            "Core.Username.InvalidUsername.Title",
            "Core.Username.InvalidUsername.Description");
    }
}