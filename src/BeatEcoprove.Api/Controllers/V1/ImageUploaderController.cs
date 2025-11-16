using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.ImageUpload.Commands;
using BeatEcoprove.Contracts.ImageUpload;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace BeatEcoprove.Api.Controllers.V1;

public sealed class ImageUploaderController : ApiCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var uploader = CreateVersionedGroup(app, "upload")
            .WithTags("Upload Imagens")
            .RequireAuthorization();

        uploader.MapPost(string.Empty, UploadImage)
            .RequireScopes("upload:action")
            .DisableAntiforgery();
    }

    private static async Task<IResult> UploadImage(
        ISender sender,
        IMapper mapper,
        IFormFile? image,
        [FromForm] string itemId,
        [FromForm] string bucket,
        HttpContext context,
        CancellationToken cancellationToken)
    {
        if (image == null || image.Length == 0)
        {
            return Results.BadRequest("No file uploaded");
        }

        var result = await sender
            .Send(new UploadImageCommand(
                Guid.Parse(itemId),
                bucket,
                image!.OpenReadStream()
            ), cancellationToken);

        return result.Match(
            url => Results.Created(
                    url.Url,
                    mapper.Map<ImageUploadResponse>(url)),
            errors => errors.ToProblemDetails(context)
        );
    }
}