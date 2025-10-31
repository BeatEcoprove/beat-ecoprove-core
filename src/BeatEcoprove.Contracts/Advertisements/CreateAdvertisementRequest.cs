using BeatEcoprove.Contracts.Common;

using Microsoft.AspNetCore.Http;

namespace BeatEcoprove.Contracts.Advertisements;

public record CreateAdvertisementRequest
(
    string Title,
    string Description,
    DateOnly BeginAt,
    DateOnly EndAt,
    string Type,
    int Quantity = 0
);