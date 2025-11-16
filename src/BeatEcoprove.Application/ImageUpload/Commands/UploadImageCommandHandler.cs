using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Helpers;
using BeatEcoprove.Application.Shared.Interfaces.Providers;
using BeatEcoprove.Domain.Shared.Errors;

using ErrorOr;

namespace BeatEcoprove.Application.ImageUpload.Commands;

internal sealed class UploadImageCommandHandler(
    IFileStorageProvider fileStorageProvider
) : ICommandHandler<UploadImageCommand, ErrorOr<ImageUrl>>
{
    public async Task<ErrorOr<ImageUrl>> Handle(
        UploadImageCommand request,
        CancellationToken cancellationToken
    )
    {
        var bucketName = Buckets.GetBucketFromDomainName(request.DomainName);

        if (bucketName is null)
        {
            return Errors.Image.InvalidBucketName;
        }

        var imageUrl = await fileStorageProvider
            .UploadFileAsync(
                bucketName,
                request.ItemId.ToString(),
                request.Image,
                cancellationToken
            );

        return new ImageUrl(
            imageUrl
        );
    }
}