using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Profiles.Commands.CreateProfile;
using BeatEcoprove.Application.Shared.Inputs;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Application.Stores.Queries.GetAdevertById;
using BeatEcoprove.Application.Stores.Queries.GetHomeAdds;
using BeatEcoprove.Contracts.Advertisements;
using BeatEcoprove.Contracts.Profile;

using MapsterMapper;

using MediatR;

namespace BeatEcoprove.Api.Controllers.V1;

public class PublicAdvertController : ApiCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var publicAdvert = CreateVersionedGroup(app, "public/adverts")
            .RequireAuthorization();

        publicAdvert.MapGet("{advertId:guid}", GetById);
        publicAdvert.MapGet(string.Empty, GetHomeAds);
    }
    
    private static async Task<IResult> GetById(
        ISender sender, 
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid advertId,
        CancellationToken cancellationToken) {
        var profileId = context.User.GetProfileId();
                        
        var result = await sender.Send(new
                GetAdvertByIdQuery(
                    profileId,
                    advertId,
                    false
                ), cancellationToken
        );
        
        return result.Match(
            profile => Results.Ok(
                mapper.Map<AdvertisementResponse>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }
    
    private static async Task<IResult> GetHomeAds(
        ISender sender, 
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        string? search,
        int? page, 
        int? pageSize,
        CancellationToken cancellationToken) {
        var profileId = context.User.GetProfileId();
                        
        var result = await sender.Send(new
                GetHomeAddsQuery(
                    profileId,
                    search,
                    page ?? 1,
                    pageSize ?? 10
                ), cancellationToken
        );
        
        return result.Match(
            profile => Results.Ok(
                mapper.Map<AdvertisementResponse>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }
}