namespace BeatEcoprove.Contracts.Advertisements;

public sealed record AdvertisementResponse(
    Guid Id,
    string Title,
    string Description,
    string Picture,
    DateTimeOffset BeginAt,
    DateTimeOffset EndAt,
    string Type
);