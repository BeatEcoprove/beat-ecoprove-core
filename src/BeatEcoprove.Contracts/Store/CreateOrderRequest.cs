namespace BeatEcoprove.Contracts.Store;

public record CreateOrderRequest(
    Guid StoreId,
    Guid OwnerId,
    Guid ClothId
);