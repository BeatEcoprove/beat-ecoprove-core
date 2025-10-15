using BeatEcoprove.Application.Closet.Common;
using BeatEcoprove.Application.Shared;

using ErrorOr;

namespace BeatEcoprove.Application.Closet.Commands.RemoveBucketFromCloset;

public record RemoveBucketFromClosetCommand
(
    Guid AuthId,
    Guid ProfileId,
    Guid BucketId
) : ICommand<ErrorOr<BucketResult>>;