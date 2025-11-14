using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Closet.Commands.RegisterClothUsage;
using BeatEcoprove.Application.Closet.Commands.RemoveClothFromOutfit;
using BeatEcoprove.Application.Closet.Queries.GetCurrentOutfit;
using BeatEcoprove.Contracts.Activities;
using BeatEcoprove.Contracts.Closet.Bucket;
using BeatEcoprove.Domain.ClosetAggregator.Entities;

using ErrorOr;

using Mapster;

using MapsterMapper;

using MediatR;

namespace BeatEcoprove.Api.Controllers.V1;

public class OutfitController : ApiCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var outfit = CreateVersionedGroup(app, "profiles/closet")
            .WithTags("Outfit")
            .RequireAuthorization();

        outfit.MapPatch("cloth/{clothId:guid}/usage", RegisterClothUsage)
            .RequireScopes("cloth:create");

        outfit.MapGet("outfit", GetCurrentOutfit)
            .RequireScopes("outfit:view");
    }

    private static Task<ErrorOr<DailyUseActivity>> UseCloth(
        ISender sender,
        Guid profileId,
        Guid clothId,
        bool use = false,
        CancellationToken cancellationToken = default
    )
    {
        return use switch
        {
            true => sender.Send(new
            {
                ProfileId = profileId,
                ClothId = clothId
            }.Adapt<RegisterClothUsageCommand>(),
                cancellationToken
            ),
            false => sender.Send(new
            {
                ProfileId = profileId,
                ClothId = clothId
            }.Adapt<RemoveClothFromOutfitCommand>(),
                cancellationToken
            ),
        };
    }

    private static async Task<IResult> RegisterClothUsage(
        ISender sender,
        IMapper mapper,
        HttpContext context,
        Guid clothId,
        bool? use,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await UseCloth(
            sender,
            profileId,
            clothId,
            use: use ?? false,
            cancellationToken
        );

        return result.Match(
            response => Results.Ok(
                mapper.Map<DailyActivityResponse>(response)),
            errors => errors.ToProblemDetails(context)
        );
    }

    private static async Task<IResult> GetCurrentOutfit(
        ISender sender,
        IMapper mapper,
        HttpContext context,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
        {
            ProfileId = profileId
        }.Adapt<GetCurrentOutfitQuery>(),
            cancellationToken
        );

        return result.Match(
            profile => Results.Ok(
                mapper.Map<BucketResponse>(profile)),
            errors => errors.ToProblemDetails(context)
        );
    }
}