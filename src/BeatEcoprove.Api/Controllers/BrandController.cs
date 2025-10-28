using Asp.Versioning;

using BeatEcoprove.Application.Brands.Queries;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Contracts.Brands;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeatEcoprove.Api.Controllers;

[ApiVersion(1)]
[Authorize]
[Route("v{version:apiVersion}/extension/brands")]
public class BrandController(
    ISender sender,
    IMapper mapper,
    ILanguageCulture languageCulture)
    : ApiController(languageCulture)
{
    [HttpGet]
    public async Task<ActionResult<BrandResponse>> GetAllBrands()
    {
        var getAllBrandsQuery =
            await sender.Send(new GetAllBrandsQuery());

        return getAllBrandsQuery.Match(
            brandResponse => Ok(mapper.Map<BrandResponse>(brandResponse)),
            Problem<BrandResponse>
        );
    }
}