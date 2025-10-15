namespace BeatEcoprove.Domain.ProfileAggregator.DAOS;

public record ProfileDao
(
    Guid Id,
    Guid AuthId,
    string Username,
    string AvatarUrl,
    string Type,
    bool IsNested
);