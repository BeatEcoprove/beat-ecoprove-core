using BeatEcoprove.Application.Currency.Common;
using BeatEcoprove.Application.Shared;

using ErrorOr;

namespace BeatEcoprove.Application.Currency.Queries.ConvertCurrency;

public record ConvertCurrencyQuery
(
    Guid ProfileId,
    int? EcoCoins,
    int? SustainabilityPoints
) : IQuery<ErrorOr<ConversionResult>>;