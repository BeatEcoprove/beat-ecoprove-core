using Asp.Versioning;

using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Currency.Queries.ConvertCurrency;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Contracts.Currency;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeatEcoprove.Api.Controllers;

[ApiVersion(1)]
[Authorize]
[Route("v{version:apiVersion}/extension/concurrency")]
public class CurrencyController(
    ISender sender,
    IMapper mapper,
    ILanguageCulture languageCulture)
    : ApiController(languageCulture)
{
    [HttpGet("convert")]
    public async Task<ActionResult<Conversionresult>> GetAllCurrencies(
        [FromRoute] Guid profileId,
        [FromQuery] int? ecoCoins,
        [FromQuery] int? sustainabilityPoints
        )
    {
        var authId = HttpContext.User.GetUserId();

        var getAllCurrenciesResult =
            await sender.Send(
                new ConvertCurrencyQuery(
                    profileId,
                    ecoCoins,
                    sustainabilityPoints
                ));
        
        return getAllCurrenciesResult.Match(
            currencyResponse => Ok(mapper.Map<Conversionresult>(currencyResponse)),
            Problem<Conversionresult>
        );
    }
}