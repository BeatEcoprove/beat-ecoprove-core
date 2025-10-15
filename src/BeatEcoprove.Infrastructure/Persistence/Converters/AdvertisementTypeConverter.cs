using BeatEcoprove.Domain.AdvertisementAggregator.Enumerators;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BeatEcoprove.Infrastructure.Persistence.Converters;

public class AdvertisementTypeConverter : ValueConverter<AdvertisementType, int>
{
    public AdvertisementTypeConverter() : base(
            v => v,
            v => (AdvertisementType)v)
    { }
}