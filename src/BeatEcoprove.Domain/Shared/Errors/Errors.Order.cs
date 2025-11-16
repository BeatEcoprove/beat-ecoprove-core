using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public partial class Errors
{
    public class Order
    {
        public static Error NotFound => Error.NotFound(
            "Core.Order.NotFound.Title",
            "Core.Order.NotFound.Description");

        public static Error IsAlreadyCompleted => Error.NotFound(
            "Core.Order.IsAlreadyCompleted.Title",
            "Core.Order.IsAlreadyCompleted.Description");
    }
}