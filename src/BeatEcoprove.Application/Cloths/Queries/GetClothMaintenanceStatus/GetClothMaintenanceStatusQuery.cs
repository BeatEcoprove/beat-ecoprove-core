using BeatEcoprove.Application.Cloths.Queries.Common;
using BeatEcoprove.Application.Shared;

using ErrorOr;

namespace BeatEcoprove.Application.Cloths.Queries.GetClothMaintenanceStatus;

public record GetClothMaintenanceStatusQuery
(
    Guid ProfileId,
    Guid ClothId
) : IQuery<ErrorOr<ClothMaintenanceStatus>>;