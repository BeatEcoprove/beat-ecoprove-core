namespace BeatEcoprove.Contracts.Store;

public record CompleteOrderRequest(
    Guid StoreId,
    Guid OwnerId
);