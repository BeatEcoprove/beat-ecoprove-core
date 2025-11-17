using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Helpers;
using BeatEcoprove.Application.Shared.Interfaces.Providers;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Errors;

using ErrorOr;

namespace BeatEcoprove.Application.ImageUpload.Commands;

internal sealed class UploadImageCommandHandler(
    IFileStorageProvider fileStorageProvider
) : ICommandHandler<UploadImageCommand, ErrorOr<UploadedUrl>>
{
    public async Task<ErrorOr<UploadedUrl>> Handle(
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
                ProfileId.CreateUnique().Value.ToString(),
                request.Image,
                cancellationToken
            );

        return new UploadedUrl(
            imageUrl
        );
    }
}