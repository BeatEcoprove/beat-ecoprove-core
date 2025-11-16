using BeatEcoprove.Domain.Shared.Errors;

using ErrorOr;

namespace BeatEcoprove.Application.Shared.Interfaces.Helpers;

public sealed class ImageUrl
{
    private ImageUrl() { }

    private ImageUrl(string bucket, string id, string url)
    {
        Bucket = bucket;
        Id = id;
        Url = url[1..];
    }

    public static ErrorOr<ImageUrl> Create(Uri url)
    {
        var segments = url.Segments;

        if (segments.Length < 4)
        {
            return Errors.Image.InvalidImage;
        }

        var bucketName = segments[2].TrimEnd('/');

        var fileName = segments[3];
        var id = Path.GetFileNameWithoutExtension(fileName);

        return new ImageUrl(bucketName, id, url.AbsolutePath);
    }

    public string Bucket { get; set; }

    public string Id { get; set; }

    public string Url { get; set; }
}