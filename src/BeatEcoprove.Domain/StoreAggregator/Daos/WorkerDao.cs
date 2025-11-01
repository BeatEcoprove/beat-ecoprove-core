using BeatEcoprove.Domain.StoreAggregator.ValueObjects;

namespace BeatEcoprove.Domain.StoreAggregator.Daos;

public record WorkerDao
(
    WorkerId Id,
    string Username,
    string Role
);