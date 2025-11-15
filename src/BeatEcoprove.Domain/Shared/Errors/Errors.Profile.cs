using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public static partial class Errors
{
    public class Profile
    {
        public static Error CannotFindCloth => Error.Conflict(
            "Core.Profile.CannotFindCloth.Title",
            "Core.Profile.CannotFindCloth.Description");

        public static Error CannotFindBucket => Error.Conflict(
            "Core.Profile.CannotFindBucket.Title",
            "Core.Profile.CannotFindBucket.Description");
        public static Error CannotExchageWithYourSelf => Error.Conflict(
            "Core.Profile.CannotExchangeWithYourSelf.Title",
            "Core.Profile.CannotExchangeWithYourSelf.Description");

        public static Error CannotConvertNegativeEcoCoins => Error.Conflict(
            "Core.Profile.CannotConvertNegativeEcoCoins.Title",
            "Core.Profile.CannotConvertNegativeEcoCoins.Description");

        public static Error NotEnoughEcoCoins => Error.Conflict(
            "Core.Profile.NotEnoughEcoCoins.Title",
            "Core.Profile.NotEnoughEcoCoins.Description");

        public static Error NotFound => Error.Conflict(
            "Core.Profile.NotFound.Title",
            "Core.Profile.NotFound.Description");

        public static Error AlreadyExists => Error.Conflict(
            "Core.Profile.AlreadyExists.Title",
            "Core.Profile.AlreadyExists.Description");
    }
}