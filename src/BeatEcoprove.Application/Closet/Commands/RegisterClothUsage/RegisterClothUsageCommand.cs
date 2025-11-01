using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.ClosetAggregator.Entities;

using ErrorOr;

namespace BeatEcoprove.Application.Closet.Commands.RegisterClothUsage;

public record RegisterClothUsageCommand
(
    Guid ProfileId,
    Guid ClothId
) : ICommand<ErrorOr<DailyUseActivity>>;