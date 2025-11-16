using BeatEcoprove.Application.Shared.Interfaces.Providers;

using Microsoft.Extensions.Options;

namespace BeatEcoprove.Infrastructure.FileStorage;

public class LocalFileStorageProvider(
    IOptions<LocalFileStorageSettings> settings
) : IFileStorageProvider
{
    private const string DefaultImagePath = "default\\default.png";
    private readonly LocalFileStorageSettings _settings = settings.Value;

    public async Task<bool> IsAlreadyCreated(
        string bucket,
        string fileName,
        CancellationToken cancellationToken = default
    )
    {
        await Task.CompletedTask;

        var pointerName = Path.Combine(bucket, fileName + ".png");
        var path = Path.Combine(_settings.FolderPath, pointerName);

        return File.Exists(path);
    }

    public async Task<string> UploadFileAsync(
        string bucketName,
        string fileName,
        Stream stream,
        CancellationToken cancellationToken = default
    )
    {
        var pointerName = Path.Combine(bucketName, fileName + ".png");
        var bucketPath = Path.Combine(_settings.FolderPath, bucketName);

        if (!Directory.Exists(bucketPath))
        {
            Directory.CreateDirectory(bucketPath);
        }

        if (stream == Stream.Null)
        {
            return Path.Combine(_settings.PublicFolder, DefaultImagePath);
        }

        var path = Path.Combine(_settings.FolderPath, pointerName);

        await using var fileStream = new FileStream(path, stream.CanSeek ? FileMode.Create : FileMode.CreateNew);
        await stream.CopyToAsync(fileStream, cancellationToken);

        return Path.Combine(_settings.PublicFolder, pointerName);
    }
}