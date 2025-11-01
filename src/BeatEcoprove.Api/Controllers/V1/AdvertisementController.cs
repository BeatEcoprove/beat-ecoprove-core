using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Application.Stores.Commands.CreateAdd;
using BeatEcoprove.Application.Stores.Commands.RemoveAdvert;
using BeatEcoprove.Application.Stores.Queries.GetAdevertById;
using BeatEcoprove.Application.Stores.Queries.GetMyAdverts;
using BeatEcoprove.Contracts.Advertisements;

using MapsterMapper;

using MediatR;

namespace BeatEcoprove.Api.Controllers.V1;

public class AdvertisementController : ApiCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var advertisement = CreateVersionedGroup(app, "stores/{storeId:guid}/adverts")
            .RequireAuthorization();

        advertisement.MapGet(string.Empty, GetMyAdverts);
        advertisement.MapGet("{advertId:guid}", GetAdvertById);
        advertisement.MapPost(string.Empty, CreateAdvert);
        advertisement.MapDelete("{advertId:guid}", DeleteAdvert);
    }

    private static async Task<IResult> GetMyAdverts(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid storeId,
        string? search,
        int? page,
        int? pageSize,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
                GetMyAdvertsQuery(
                    profileId,
                    storeId,
                    search,
                    page ?? 1,
                    pageSize ?? 10
                ), cancellationToken
        );

        return result.Match(
            profile => Results.Ok(
                mapper.Map<List<AdvertisementResponse>>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> GetAdvertById(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid advertId,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
                GetAdvertByIdQuery(
                    profileId,
                    advertId
                ), cancellationToken
        );

        return result.Match(
            profile => Results.Ok(
                mapper.Map<AdvertisementResponse>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> CreateAdvert(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid storeId,
        CreateAdvertisementRequest request,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
            CreateAddCommand(
                profileId,
                storeId,
                request.Title,
                request.Description,
                request.BeginAt,
                request.EndAt,
                request.Type,
                request.Quantity
            ), cancellationToken
        );

        return result.Match(
            profile => Results.Ok(
                mapper.Map<AdvertisementResponse>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> DeleteAdvert(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid storeId,
        Guid advertId,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
            RemoveAdvertCommand(
                profileId,
                advertId
            ), cancellationToken
        );

        return result.Match(
            profile => Results.Ok(
                mapper.Map<AdvertisementResponse>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }
}