using BeatEcoprove.Domain.StoreAggregator.Enumerators;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BeatEcoprove.Infrastructure.Persistence.Converters;

public class OrderTypeConverter : ValueConverter<OrderType, int>
{
    public OrderTypeConverter() : base(
            v => v,
            v => (OrderType)v)
    { }
}