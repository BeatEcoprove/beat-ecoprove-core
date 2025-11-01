namespace BeatEcoprove.Contracts.Closet.Bucket;

public record CreateClothToBucketRequest
(
    List<Guid> ClothToAdd
);