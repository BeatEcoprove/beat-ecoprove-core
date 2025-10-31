using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.ClosetAggregator;

using ErrorOr;

namespace BeatEcoprove.Application.ClosetBuckets.Commands.PatchBucket;

public record PatchBucketCommand
(
    Guid ProfileId,
    Guid BucketId,
    string Name
) : ICommand<ErrorOr<Bucket>>;