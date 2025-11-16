using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public partial class Errors
{
    public class Provider
    {
        public static Error NotFound => Error.NotFound(
            "Core.Provider.NotFound.Title",
            "Core.Provider.NotFound.Description");
    }
}