using BeatEcoprove.Application.Shared;

using ErrorOr;

namespace BeatEcoprove.Application.ImageUpload.Commands;

public sealed record UploadedUrl(string Url);
public sealed record UploadImageCommand(
    string DomainName,
    Stream Image
) : ICommand<ErrorOr<UploadedUrl>>;