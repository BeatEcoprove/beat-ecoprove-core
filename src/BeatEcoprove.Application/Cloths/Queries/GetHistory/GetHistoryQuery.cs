using BeatEcoprove.Application.Cloths.Queries.Common.HistoryResult;
using BeatEcoprove.Application.Shared;

using ErrorOr;

namespace BeatEcoprove.Application.Cloths.Queries.GetHistory;

public record GetHistoryQuery
(
    Guid ProfileId,
    Guid ClothId
) : IQuery<ErrorOr<List<HistoryResult>>>;