using BeatEcoprove.Application.Closet.Common;
using BeatEcoprove.Application.Shared;

using ErrorOr;

namespace BeatEcoprove.Application.Closet.Queries.GetBucketsCloth;

public record GetBucketsClothQuery
(
    Guid AuthId,
    Guid ProfileId,
    Guid ClothId
) : IQuery<ErrorOr<List<BucketResult>>>;