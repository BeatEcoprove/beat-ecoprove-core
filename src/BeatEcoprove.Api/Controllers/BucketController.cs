using Asp.Versioning;

using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.ClosetBuckets.Commands;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Contracts.Closet.Bucket;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeatEcoprove.Api.Controllers;


[ApiVersion(1)]
[Authorize]
[Route("v{version:apiVersion}/profiles/closet/bucket/{bucketId:guid}")]
public class BucketController(
    ISender sender,
    IMapper mapper,
    ILanguageCulture languageCulture)
    : ApiController(languageCulture)
{
    [HttpPatch]
    public async Task<ActionResult<BucketResponse>> PathBucket(
        Guid bucketId,
        [FromQuery] Guid profileId,
        [FromBody] PatchBucketRequest request)
    {
        var authId = HttpContext.User.GetUserId();

        var result = await sender.Send(new
            PatchBucketCommand(
                authId,
                profileId,
                bucketId,
                request.Name
            ));

        return result.Match(
            response => Ok(mapper.Map<BucketResponse>(response)),
            Problem<BucketResponse>
        );
    }
}