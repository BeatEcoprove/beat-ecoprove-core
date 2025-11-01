using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Interfaces.Persistence;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.ClosetAggregator;
using BeatEcoprove.Domain.ClosetAggregator.ValueObjects;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Errors;

using ErrorOr;

namespace BeatEcoprove.Application.ClosetBuckets.Commands.PatchBucket;

internal sealed class PatchBucketCommandHandler(
    IProfileManager profileManager,
    IProfileRepository profileRepository,
    IBucketRepository bucketRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<PatchBucketCommand, ErrorOr<Bucket>>
{
    public async Task<ErrorOr<Bucket>> Handle(PatchBucketCommand request, CancellationToken cancellationToken)
    {
        var profileId = ProfileId.Create(request.ProfileId);
        var bucketId = BucketId.Create(request.BucketId);

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return Errors.Bucket.NameCannotBeEmpty;
        }

        var profile = await profileManager.GetProfileAsync(profileId, cancellationToken);

        if (profile.IsError)
        {
            return profile.Errors;
        }

        if (!await profileRepository.CanProfileAccessBucket(profile.Value.Id, bucketId, cancellationToken))
        {
            return Errors.Bucket.CannotAccessBucket;
        }

        if (await bucketRepository.IsBucketNameAlreadyUsed(profile.Value.Id, bucketId, request.Name, cancellationToken))
        {
            return Errors.Bucket.BucketNameAlreadyUsed;
        }

        var bucket = await bucketRepository.GetByIdAsync(bucketId, cancellationToken);

        if (bucket is null)
        {
            return Errors.Bucket.BucketDoesNotExists;
        }

        bucket.SetName(request.Name);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return bucket;
    }
}