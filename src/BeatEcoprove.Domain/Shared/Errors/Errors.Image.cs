
using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public partial class Errors
{
    public class Image
    {
        public static Error InvalidBucketName => Error.Conflict(
            "Core.Image.ImavalidBucketName.Title",
            "Core.Image.ImavalidBucketName.Description");

        public static Error InvalidImage => Error.Conflict(
            "Core.Image.InvalidImage.Title",
            "Core.Image.InvalidImage.Description");
    }
}