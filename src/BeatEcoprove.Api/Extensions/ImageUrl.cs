using BeatEcoprove.Application.ImageUpload.Commands;

namespace BeatEcoprove.Api.Extensions;

public static class UploadedUrlExtensions
{
    public static ImageUrl Format(this UploadedUrl uploadedUrl, HttpContext context)
        => ImageUrl.Create(uploadedUrl.Url, context);
}

public sealed class ImageUrl
{
    private readonly string _url;
    private readonly HttpContext _context;

    private ImageUrl() { }

    private ImageUrl(string url, HttpContext context)
    {
        this._url = url;
        this._context = context;
    }

    public static ImageUrl Create(string url, HttpContext context)
        => new(url, context);

    public string Url => $"{_context.Request.Scheme}://{_context.Request.Host}/{_url}";

    public static implicit operator string(ImageUrl url)
        => url.Url;
}