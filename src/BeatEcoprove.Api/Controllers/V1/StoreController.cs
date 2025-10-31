using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Application.Stores.Commands.AddStore;
using BeatEcoprove.Application.Stores.Commands.DeleteStoreById;
using BeatEcoprove.Application.Stores.Queries.GetOwningStores;
using BeatEcoprove.Application.Stores.Queries.GetStoreById;
using BeatEcoprove.Contracts.Store;

using MapsterMapper;

using MediatR;

namespace BeatEcoprove.Api.Controllers.V1;

public class StoreController : ApiCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var stores = CreateVersionedGroup(app, "stores")
            .RequireAuthorization();

        stores.MapGet(string.Empty, GetOwningStores);
        stores.MapGet("{storeId:guid}", GetStoreById);
        stores.MapPost(string.Empty, CreateStore);
        stores.MapDelete("{storeId:guid}", DeleteStoreById);
    }
    
    private static async Task<IResult> GetOwningStores(
        ISender sender, 
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        string? search, 
        int page, 
        int pageSize,
        CancellationToken cancellationToken) {
        var profileId = context.User.GetProfileId();
                        
        var result = await sender.Send(new
                GetOwningStoresQuery(
                    profileId,
                    search,
                    page,
                    pageSize
                ), cancellationToken
        );
        
        return result.Match(
            profile => Results.Ok(
                mapper.Map<List<StoreResponse>>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }
    
    private static async Task<IResult> GetStoreById(
        ISender sender, 
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid storeId,
        CancellationToken cancellationToken) {
        var profileId = context.User.GetProfileId();
                        
        var result = await sender.Send(new
                GetStoreByIdQuery(
                    profileId,
                    storeId
                ), cancellationToken
        );
        
        return result.Match(
            profile => Results.Ok(
                mapper.Map<StoreResponse>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }
    
    private static async Task<IResult> CreateStore(
        ISender sender, 
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        CreateStoreRequest request,
        CancellationToken cancellationToken) {
        var profileId = context.User.GetProfileId();
                        
        var result = await sender.Send(new
                AddStoreCommand(
                    profileId,
                    request.Name,
                    request.Country,
                    request.Locality,
                    request.Street,
                    request.ZipCode,
                    request.Port,
                    request.Lat,
                    request.Lon
                ), cancellationToken
        );
        
        return result.Match(
            profile => Results.Ok(
                mapper.Map<StoreResponse>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }
    
    private static async Task<IResult> DeleteStoreById(
        ISender sender, 
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid storeId,
        CancellationToken cancellationToken) {
        var profileId = context.User.GetProfileId();
                        
        var result = await sender.Send(new
            DeleteStoreByIdCommand(
                profileId,
                storeId
            ), cancellationToken
        );
        
        return result.Match(
            profile => Results.Ok(
                mapper.Map<StoreResponse>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }
}