using BeatEcoprove.Application.Shared;

using ErrorOr;

namespace BeatEcoprove.Application.ImageUpload.Commands;

public sealed record ImageUrl(string Url);

public sealed record UploadImageCommand(
    Guid ItemId,
    string DomainName,
    Stream Image
) : ICommand<ErrorOr<ImageUrl>>;