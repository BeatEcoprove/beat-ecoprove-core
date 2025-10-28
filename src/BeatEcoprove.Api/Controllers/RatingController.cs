using Asp.Versioning;

using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Application.Stores.Commands.PostRating;
using BeatEcoprove.Application.Stores.Queries.GetStoreRatings;
using BeatEcoprove.Contracts.Store;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeatEcoprove.Api.Controllers;

[ApiVersion(1)]
[Authorize]
[Route("v{version:apiVersion}/stores/{storeId:guid}/ratings")]
public class RatingController(
    ILanguageCulture localizer,
    ISender sender,
    IMapper mapper)
    : ApiController(localizer)
{
    [HttpGet]
    public async Task<ActionResult<List<RatingResponse>>> GetStoreRatings(
        [FromQuery] Guid profileId,
        [FromRoute] Guid storeId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default
    ) {
        var authId = HttpContext.User.GetUserId();
                        
        var getRatings = await sender.Send(new
            GetStoreRatingsQuery(
                profileId,
                storeId,
                page,
                pageSize
            ), cancellationToken
        );
        
        return getRatings.Match(
            result => Ok(mapper.Map<List<RatingResponse>>(result)),
            Problem<List<RatingResponse>>
        );
    }
    
    [HttpPost]
    public async Task<ActionResult<RatingResponse>> PostRating(
        [FromQuery] Guid profileId,
        [FromRoute] Guid storeId,
        CreatePostRating request,
        CancellationToken cancellationToken = default
    ) {
        var authId = HttpContext.User.GetUserId();
                
        var postRating = await sender.Send(new
            PostRatingCommand(
                profileId,
                storeId,
                request.Rating
            ), cancellationToken
        );
        
        return postRating.Match(
            result => Ok(mapper.Map<RatingResponse>(result)),
            Problem<RatingResponse>
        );
    }
}