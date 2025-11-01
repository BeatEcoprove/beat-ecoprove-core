using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Closet.Commands.AddClothToBucket;
using BeatEcoprove.Application.Closet.Commands.CreateBucket;
using BeatEcoprove.Application.Closet.Commands.CreateCloth;
using BeatEcoprove.Application.Closet.Commands.RemoveBucketFromCloset;
using BeatEcoprove.Application.Closet.Commands.RemoveClothFromBucket;
using BeatEcoprove.Application.Closet.Commands.RemoveClothFromCloset;
using BeatEcoprove.Application.Closet.Queries.GetBucket;
using BeatEcoprove.Application.Closet.Queries.GetBucketsCloth;
using BeatEcoprove.Application.Closet.Queries.GetCloset;
using BeatEcoprove.Application.Closet.Queries.GetCloth;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Contracts.Closet;
using BeatEcoprove.Contracts.Closet.Bucket;
using BeatEcoprove.Contracts.Closet.Cloth;

using Mapster;

using MapsterMapper;

using MediatR;

namespace BeatEcoprove.Api.Controllers.V1;

public class ClosetController : ApiCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var closet = CreateVersionedGroup(app, "profiles/closet")
            .RequireAuthorization();

        closet.MapGet(string.Empty, GetCloset);

        var closetCloth = closet.MapGroup("cloths");

        closetCloth.MapPost(String.Empty, CreateClothToCloset)
            .RequireScopes("cloth:create");
        
        closetCloth.MapGet("{clothId:guid}/buckets", GetClothBuckets)
            .RequireScopes("cloth:view");
        
        closetCloth.MapGet("{clothId:guid}", GetClothById)
            .RequireScopes("cloth:view");
        
        closetCloth.MapDelete("{clothId:guid}", DeleteClothFromCloset)
            .RequireScopes("cloth:delete");

        var closetBucket = closet.MapGroup("buckets");

        closetBucket.MapPost(String.Empty, CreateBucketToCloset)
            .RequireScopes("bucket:create");
        
        closetBucket.MapGet("{bucketId:guid}", GetBucketById)
            .RequireScopes("bucket:view");
        
        closetBucket.MapDelete("{bucketId:guid}", DeleteBucketFromCloset)
            .RequireScopes("bucket:delete");

        var clothBucket = closetCloth.MapGroup("buckets");

        clothBucket.MapPost("{bucketId:guid}", CreateClothsToBucket)
            .RequireScopes("bucket:create");
        
        clothBucket.MapPut("{bucketId:guid}", DeleteClothFromBucket)
            .RequireScopes("bucket:delete");
    }

    private static async Task<IResult> GetCloset(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        string? search,
        string? size,
        string? category,
        string? color,
        string? brand,
        string? orderBy,
        string? sortBy,
        int? page,
        int? pageSize,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result =
            await sender.Send(new
            {
                ProfileId = profileId,
                Search = search,
                Categories = category,
                Size = size,
                Color = color,
                Brand = brand,
                OrderBy = orderBy,
                SortBy = sortBy,
                Page = page,
                PageSize = pageSize
            }.Adapt<GetClosetQuery>(), cancellationToken);

        return result.Match(
            response => Results.Ok(
                mapper.Map<ClosetResponse>(response)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> CreateClothToCloset(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        CreateClothRequest request,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result =
            await sender.Send(new CreateClothCommand(
                    profileId,
                    request.Name,
                    request.ClothType,
                    request.ClothSize,
                    request.Brand,
                    request.Color),
                cancellationToken
            );

        return result.Match(
            response => Results.Created(
                "profiles/closet",
                mapper.Map<ClothResponse>(response)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> GetClothBuckets(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid clothId,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result =
            await sender.Send(new
            {
                ProfileId = profileId,
                ClothId = clothId
            }.Adapt<GetBucketsClothQuery>(),
                cancellationToken
            );


        return result.Match(
            response => Results.Ok(
                mapper.Map<List<BucketResponse>>(response)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> GetClothById(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid clothId,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result =
            await sender.Send(new
            {
                ProfileId = profileId,
                ClothId = clothId
            }.Adapt<GetClothQuery>(),
                cancellationToken
            );

        return result.Match(
            response => Results.Ok(
                mapper.Map<ClothResponse>(response)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> DeleteClothFromCloset(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid clothId,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result =
            await sender.Send(new
            {
                ProfileId = profileId,
                ClothId = clothId
            }.Adapt<RemoveClothFromClosetCommand>(),
                cancellationToken
            );


        return result.Match(
            response => Results.Ok(
                response.Adapt<ClothResponse>()),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> CreateBucketToCloset(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        CreateBucketRequest request,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result =
            await sender.Send(new
            {
                ProfileId = profileId,
                request.Name,
                request.ClothIds
            }.Adapt<CreateBucketCommand>(),
                cancellationToken
            );

        return result.Match(
            response => Results.Created(
                "",
                mapper.Map<BucketResponse>(response)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> GetBucketById(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid bucketId,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result =
            await sender.Send(new
            {
                ProfileId = profileId,
                BucketId = bucketId
            }.Adapt<GetBucketQuery>(),
                cancellationToken
            );

        return result.Match(
            response => Results.Ok(
                mapper.Map<BucketResponse>(response)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> DeleteBucketFromCloset(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid bucketId,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result =
            await sender.Send(new
            {
                ProfileId = profileId,
                BucketId = bucketId
            }.Adapt<RemoveBucketFromClosetCommand>(),
                cancellationToken
            );

        return result.Match(
            response => Results.Ok(
                mapper.Map<BucketResponse>(response)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> CreateClothsToBucket(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid bucketId,
        CreateClothToBucketRequest request,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result =
            await sender.Send(new
            {
                ProfileId = profileId,
                BucketId = bucketId,
                request.ClothToAdd
            }.Adapt<AddClothToBucketCommand>(), cancellationToken);

        return result.Match(
            response => Results.Created(
                "",
                mapper.Map<BucketResponse>(response)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> DeleteClothFromBucket(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid bucketId,
        DeleteClothsFromBucketRequest request,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result =
            await sender.Send(new
            {
                ProfileId = profileId,
                BucketId = bucketId,
                Cloths = request.Cloths,
            }.Adapt<RemoveClothFromBucketCommand>(),
                cancellationToken
            );

        return result.Match(
            response => Results.Ok(
                mapper.Map<BucketResponse>(response)),
            errors => errors.ToProblemDetails(localizer)
        );
    }
}