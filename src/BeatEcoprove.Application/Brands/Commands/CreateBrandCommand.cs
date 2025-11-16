
using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.Shared.Entities;

using ErrorOr;

namespace BeatEcoprove.Application.Brands.Commands;

public sealed record CreateBrandCommand(
    string Name,
    Uri Picture
) : ICommand<ErrorOr<Brand>>;