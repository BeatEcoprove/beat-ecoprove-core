using Asp.Versioning;

using BeatEcoprove.Application.Colors.Queries;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Contracts.Colors;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeatEcoprove.Api.Controllers;

[ApiVersion(1)]
[Authorize]
[Route("v{version:apiVersion}/extension/colors")]
public class ColorController(
    ISender sender,
    IMapper mapper,
    ILanguageCulture languageCulture)
    : ApiController(languageCulture)
{
    [HttpGet]
    public async Task<ActionResult<ColorResponse>> GetAllColors()
    {
        var getAllColorsResult =
            await sender.Send(new GetColorsQuery());

        return getAllColorsResult.Match(
            colorResponse => Ok(mapper.Map<ColorResponse>(colorResponse)),
            Problem<ColorResponse>
        );
    }
}