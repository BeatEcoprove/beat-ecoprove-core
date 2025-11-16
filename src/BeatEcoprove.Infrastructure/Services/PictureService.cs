using BeatEcoprove.Application.Shared.Interfaces.Helpers;
using BeatEcoprove.Application.Shared.Interfaces.Providers;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.Shared.Errors;

using ErrorOr;

namespace BeatEcoprove.Infrastructure.Services;

public sealed class PictureService(
    IFileStorageProvider fileStorageProvider
) : IPictureService
{
    public async Task<ErrorOr<string>> GetImageUrlLiteral(
        Uri pictureUrl,
        CancellationToken cancellationToken = default)
    {
        var image = ImageUrl.Create(pictureUrl);

        if (image.IsError)
            return image.Errors;

        if (!await fileStorageProvider.IsAlreadyCreated(
                image.Value.Bucket,
                image.Value.Id, 
                cancellationToken))
            return Errors.Brand.MustProvideBrandAvatar;

        return image.Value.Url;
    }
}