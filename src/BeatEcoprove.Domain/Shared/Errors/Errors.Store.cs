using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public partial class Errors
{
    public class Store
    {
        public static Error RateNotAllowed => Error.Conflict(
            "Core.Store.RateNotAllowed.Title",
            "Core.Store.RateNotAllowed.Description");

        public static Error CantCreateStore => Error.Conflict(
            "Core.Store.CantCreateStore.Title",
            "Core.Store.CantCreateStore.Description");

        public static Error StoreNotFound => Error.NotFound(
            "Core.Store.StoreNotFound.Title",
            "Core.Store.StoreNotFound.Description");
        public static Error CouldNotDelete => Error.NotFound(
            "Core.Store.CouldNotDelete.Title",
            "Core.Store.CouldNotDelete.Description");
        public static Error StoreAlreadyExistsName => Error.Conflict(
            "Core.Store.StoreAlreadyExistsName.Title",
            "Core.Store.StoreAlreadyExistsName.Description");

        public static Error DontHaveAccessToStore => Error.Conflict(
            "Core.Store.DontHaveAccessToStore.Title",
            "Core.Store.DontHaveAccessToStore.Description");
    }
}