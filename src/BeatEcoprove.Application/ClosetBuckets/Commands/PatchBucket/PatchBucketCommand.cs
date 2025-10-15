using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.ClosetAggregator;

using ErrorOr;

namespace BeatEcoprove.Application.ClosetBuckets.Commands;

public record PatchBucketCommand
(
    Guid AuthId,
    Guid ProfileId,
    Guid BucketId,
    string Name
) : ICommand<ErrorOr<Bucket>>;