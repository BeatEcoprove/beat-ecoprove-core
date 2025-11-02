using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Currency.Queries.ConvertCurrency;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Contracts.Currency;

using MapsterMapper;

using MediatR;

namespace BeatEcoprove.Api.Controllers.V1;

public class CurrencyController : ApiCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var currency = CreateVersionedGroup(app, "extensions/currency")
            .WithName("Currency")
            .RequireAuthorization();

        currency.MapGet("convert", async (
            ISender sender,
            IMapper mapper,
            ILanguageCulture localizer,
            HttpContext context,
            int? ecoCoins,
            int? sustainabilityPoints
        ) =>
        {
            var profileId = context.User.GetProfileId();

            var result =
                await sender.Send(
                    new ConvertCurrencyQuery(
                        profileId,
                        ecoCoins,
                        sustainabilityPoints
                    ));

            return result.Match(
                response => Results.Ok(
                    mapper.Map<Conversionresult>(response)),
                errors => errors.ToProblemDetails(localizer)
            );
        }).RequireScopes("currency:convert");
    }
}