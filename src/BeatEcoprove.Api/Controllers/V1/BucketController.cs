using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.ClosetBuckets.Commands.PatchBucket;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Contracts.Closet.Bucket;

using MapsterMapper;

using MediatR;

namespace BeatEcoprove.Api.Controllers.V1;

public class BucketController : ApiCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var buckets = CreateVersionedGroup(app, "profiles/closet/buckets")
            .RequireAuthorization();

        buckets.MapPatch("{bucketId:guid}", async (
            ISender sender,
            IMapper mapper,
            ILanguageCulture localizer,
            HttpContext context,
            Guid bucketId,
            PatchBucketRequest request,
            CancellationToken cancellationToken
        ) =>
        {
            var profileId = context.User.GetProfileId();

            var result = await sender.Send(new
                PatchBucketCommand(
                    profileId,
                    bucketId,
                    request.Name
                ), cancellationToken);

            return result.Match(
                response => Results.Ok(
                    mapper.Map<BucketResponse>(response)),
                errors => errors.ToProblemDetails(localizer)
            );
        }).RequireScopes("bucket:update");
    }
}