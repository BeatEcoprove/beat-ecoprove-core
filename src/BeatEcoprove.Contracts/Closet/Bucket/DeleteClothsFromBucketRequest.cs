namespace BeatEcoprove.Contracts.Closet.Bucket;

public record DeleteClothsFromBucketRequest(
    List<Guid> Cloths
);