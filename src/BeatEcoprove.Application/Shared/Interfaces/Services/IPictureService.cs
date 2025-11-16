using ErrorOr;

namespace BeatEcoprove.Application.Shared.Interfaces.Services;

public interface IPictureService
{
    Task<ErrorOr<string>> GetImageUrlLiteral(
        Uri pictureUrl,
        CancellationToken cancellationToken = default);
}