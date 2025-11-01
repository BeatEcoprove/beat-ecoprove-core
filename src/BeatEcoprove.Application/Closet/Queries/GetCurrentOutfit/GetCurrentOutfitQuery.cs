using BeatEcoprove.Application.Closet.Common;
using BeatEcoprove.Application.Shared;

using ErrorOr;

namespace BeatEcoprove.Application.Closet.Queries.GetCurrentOutfit;

public record GetCurrentOutfitQuery
(
    Guid ProfileId
) : IQuery<ErrorOr<BucketResult>>;