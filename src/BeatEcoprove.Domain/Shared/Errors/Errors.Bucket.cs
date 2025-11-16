using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public static partial class Errors
{
    public class Bucket
    {
        public static Error InvalidClothToAdd => Error.Conflict(
            "Core.Bucket.InvalidClothToAdd.Title",
            "Core.Bucket.InvalidClothToAdd.Description");

        public static Error EmptyClothIds => Error.Conflict(
            "Core.Bucket.EmptyClothIds.Title",
            "Core.Bucket.EmptyClothIds.Description");

        public static Error ClothAreNotUnique => Error.Conflict(
            "Core.Bucket.ClothAreNotUnique.Title",
            "Core.Bucket.ClothAreNotUnique.Description");

        public static Error CanAddClothToBucket => Error.Conflict(
            "Core.Bucket.CanAddClothToBucket.Title",
            "Core.Bucket.CanAddClothToBucket.Description");

        public static Error CannotRemoveCloth => Error.Conflict(
            "Core.Bucket.CannotRemoveCloth.Title",
            "Core.Bucket.CannotRemoveCloth.Description");

        public static Error BucketNameAlreadyUsed => Error.Conflict(
            "Core.Bucket.BucketNameAlreadyUsed.Title",
            "Core.Bucket.BucketNameAlreadyUsed.Description");

        public static Error InvalidBucketName => Error.Validation(
            "Core.Bucket.InvalidBucketName.Title",
            "Core.Bucket.InvalidBucketName.Description");

        public static Error BucketDoesNotExists => Error.Conflict(
            "Core.Bucket.BucketDoesNotExists.Title",
            "Core.Bucket.BucketDoesNotExists.Description");

        public static Error CannotAccessBucket => Error.Conflict(
            "Core.Bucket.CannotAccessBucket.Title",
            "Core.Bucket.CannotAccessBucket.Description");

        public static Error NameCannotBeEmpty => Error.Validation(
            "Core.Bucket.NameCannotBeEmpty.Title",
            "Core.Bucket.NameCannotBeEmpty.Description");
    }
}