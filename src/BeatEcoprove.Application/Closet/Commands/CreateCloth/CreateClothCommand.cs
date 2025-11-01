using BeatEcoprove.Application.Closet.Common;
using BeatEcoprove.Application.Shared;

using ErrorOr;

namespace BeatEcoprove.Application.Closet.Commands.CreateCloth;

public record CreateClothCommand
(
    Guid ProfileId,
    string Name,
    string ClothType,
    string ClothSize,
    string Brand,
    string Color
) : ICommand<ErrorOr<ClothResult>>;