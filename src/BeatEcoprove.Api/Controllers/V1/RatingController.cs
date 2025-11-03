using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Stores.Commands.PostRating;
using BeatEcoprove.Application.Stores.Queries.GetStoreRatings;
using BeatEcoprove.Contracts.Store;

using MapsterMapper;

using MediatR;

namespace BeatEcoprove.Api.Controllers.V1;

public class RatingController : ApiCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var ratting = CreateVersionedGroup(app, "stores/{storeId:guid}/ratings")
            .WithTags("Ratings")
            .RequireAuthorization();

        ratting.MapGet(string.Empty, GetStoreRatings)
            .RequireScopes("ratings:view");

        ratting.MapPost(string.Empty, CreateRating)
            .RequireScopes("ratings:create");
    }

    private static async Task<IResult> GetStoreRatings(
        ISender sender,
        IMapper mapper,
        HttpContext context,
        Guid storeId,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
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
            errors => errors.ToProblemDetails(context)
        );
    }

    private static async Task<IResult> CreateRating(
        ISender sender,
        IMapper mapper,
        HttpContext context,
        Guid storeId,
        CreatePostRating request,
        CancellationToken cancellationToken)
    {
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
            errors => errors.ToProblemDetails(context)
        );
    }
}