
using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public partial class Errors
{
    public class Image
    {
        public static Error InvalidBucketName => Error.Conflict(
            "Core.Bucket.ImavalidBucketName.Title",
            "Core.Bucket.ImavalidBucketName.Description");
    }
}