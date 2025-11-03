using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public partial class Errors
{
    public class Concurrency
    {
        public static Error SymbolNotDefined => Error.Validation(
            "Core.Concurrency.SymbolNotDefined.Title",
            "Core.Concurrency.SymbolNotDefined.Description");
    }
}