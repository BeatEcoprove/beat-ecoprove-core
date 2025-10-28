using Asp.Versioning;

using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Providers.Queries.GetAllStandardProviders;
using BeatEcoprove.Application.Providers.Queries.GetProviderAdverts;
using BeatEcoprove.Application.Providers.Queries.GetProviderById;
using BeatEcoprove.Application.Providers.Queries.GetProviderStoreById;
using BeatEcoprove.Application.Providers.Queries.GetProviderStores;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Contracts.Advertisements;
using BeatEcoprove.Contracts.Providers;
using BeatEcoprove.Contracts.Store;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeatEcoprove.Api.Controllers;

[ApiVersion(1)]
[Authorize]
[Route("v{version:apiVersion}/providers")]
public class ProviderController(
    ILanguageCulture localizer,
    ISender sender,
    IMapper mapper)
    : ApiController(localizer)
{
    [HttpGet]
    public async Task<ActionResult<List<StandardProviderResponse>>> GetAllProviders(
        [FromQuery] Guid profileId,
        [FromQuery] string? search = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    { 
        var authId = HttpContext.User.GetUserId();
                        
        var getAllProviders = await sender.Send(new
            GetAllStandardProvidersQuery(
                profileId,
                search,
                page,
                pageSize
            ), cancellationToken
        );
        
        return getAllProviders.Match(
            result => Ok(mapper.Map<List<StandardProviderResponse>>(result)),
            Problem<List<StandardProviderResponse>>
        );
    }
    
    [HttpGet("{providerId:guid}")]
    public async Task<ActionResult<ProviderResponse>> GetProviderById(
        [FromQuery] Guid profileId,
        [FromRoute] Guid providerId,
        CancellationToken cancellationToken = default)
    {
        var authId = HttpContext.User.GetUserId();
                        
        var getAllProviders = await sender.Send(new
            GetProviderByIdQuery(
                profileId,
                providerId
            ), cancellationToken
        );
        
        return getAllProviders.Match(
            result => Ok(mapper.Map<ProviderResponse>(result)),
            Problem<ProviderResponse>
        );
    }

    [HttpGet("{providerId:guid}/stores")]
    public async Task<ActionResult<List<StoreResponse>>> GetStores(
        [FromQuery] Guid profileId,
        [FromRoute] Guid providerId,
        [FromQuery] string? search = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var authId = HttpContext.User.GetUserId();
                        
        var getAllProviders = await sender.Send(new
            GetProviderStoresQuery(
                profileId, 
                providerId, 
                search, 
                page, 
                pageSize
            ), cancellationToken
        );
        
        return getAllProviders.Match(
            result => Ok(mapper.Map<List<StoreResponse>>(result)),
            Problem<List<StoreResponse>>
        );
    }

    [HttpGet("{providerId:guid}/stores/{storeId:guid}")]
    public async Task<ActionResult<StoreResponse>> GetStoreById(
        [FromQuery] Guid profileId,
        [FromRoute] Guid providerId,
        [FromRoute] Guid storeId,
        CancellationToken cancellationToken = default)
    {
        var authId = HttpContext.User.GetUserId();
                        
        var getStoreById = await sender.Send(new
            GetProviderStoreByIdQuery(
                profileId,
                providerId,
                storeId
            ), cancellationToken
        );
        
        return getStoreById.Match(
            result => Ok(mapper.Map<StoreResponse>(result)),
            Problem<StoreResponse>
        );
    }

    [HttpGet("{providerId:guid}/adverts")]
    public async Task<ActionResult<List<AdvertisementResponse>>> GetProviderAdverts(
        [FromQuery] Guid profileId,
        [FromRoute] Guid providerId, 
        CancellationToken cancellationToken = default)
    {
        var authId = HttpContext.User.GetUserId();
                        
        var getAdverts = await sender.Send(new
            GetProviderAdvertsQuery(
                profileId, 
                providerId
            ), cancellationToken
        );
        
        return getAdverts.Match(
            result => Ok(mapper.Map<List<AdvertisementResponse>>(result)),
            Problem<List<AdvertisementResponse>>
        );
    }
}