using BeatEcoprove.Contracts.Common;
using Microsoft.AspNetCore.Http;

namespace BeatEcoprove.Contracts.ImageUpload;

public sealed record ImageUploadRequest
(
    Guid ItemId,
    string Bucket,
    IFormFile Image
) : IPictureRequest
{
    public Stream PictureStream => Image.OpenReadStream();
}
