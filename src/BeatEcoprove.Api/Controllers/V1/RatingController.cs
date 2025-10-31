using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Application.Stores.Commands.PostRating;
using BeatEcoprove.Application.Stores.Queries.GetAdevertById;
using BeatEcoprove.Application.Stores.Queries.GetStoreRatings;
using BeatEcoprove.Contracts.Advertisements;
using BeatEcoprove.Contracts.Store;

using MapsterMapper;

using MediatR;

namespace BeatEcoprove.Api.Controllers.V1;

public class RatingController : ApiCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var ratting = CreateVersionedGroup(app, "stores/{storeId:guid}/ratings")
            .RequireAuthorization();

        ratting.MapGet(string.Empty, GetStoreRatings);
        ratting.MapPost(string.Empty, CreateRating);
    }
    
    private static async Task<IResult> GetStoreRatings(
        ISender sender, 
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid storeId,
        int page, 
        int pageSize,
        CancellationToken cancellationToken) {
        var profileId = context.User.GetProfileId();
                        
        var result = await sender.Send(new
                GetStoreRatingsQuery(
                    profileId,
                    storeId,
                    page,
                    pageSize
                ), cancellationToken
        );
        
        return result.Match(
            profile => Results.Ok(
                mapper.Map<List<RatingResponse>>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }
    
    private static async Task<IResult> CreateRating(
        ISender sender, 
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid storeId,
        CreatePostRating request,
        CancellationToken cancellationToken) {
        var profileId = context.User.GetProfileId();
                        
        var result = await sender.Send(new
                PostRatingCommand(
                    profileId,
                    storeId,
                    request.Rating
                ), cancellationToken
        );
        
        return result.Match(
            profile => Results.Ok(
                mapper.Map<RatingResponse>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }
}