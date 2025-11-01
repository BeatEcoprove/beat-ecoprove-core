using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Providers.Queries.GetAllStandardProviders;
using BeatEcoprove.Application.Providers.Queries.GetProviderAdverts;
using BeatEcoprove.Application.Providers.Queries.GetProviderById;
using BeatEcoprove.Application.Providers.Queries.GetProviderStoreById;
using BeatEcoprove.Application.Providers.Queries.GetProviderStores;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Contracts.Advertisements;
using BeatEcoprove.Contracts.Providers;
using BeatEcoprove.Contracts.Store;

using MapsterMapper;

using MediatR;

namespace BeatEcoprove.Api.Controllers.V1;

public class ProviderController : ApiCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var providers = CreateVersionedGroup(app, "providers")
            .RequireAuthorization();

        providers.MapGet(string.Empty, GetAllProviders)
            .RequireScopes("providers:view");

        providers.MapGet("{providerId:guid}", GetProviderById)
            .RequireScopes("providers:view");

        var stores = providers.MapGroup("{providerId:guid}/stores");

        stores.MapGet(String.Empty, GetStores)
            .RequireScopes("stores:view");

        stores.MapGet("{storeId:guid}", GetStoreById)
            .RequireScopes("stores:view");

        providers.MapGet(String.Empty, GetProviderAdverts)
            .RequireScopes("adverts:view");
    }

    private static async Task<IResult> GetAllProviders(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        string? search,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
                GetAllStandardProvidersQuery(
                    profileId,
                    search,
                    page,
                    pageSize
                ), cancellationToken
        );

        return result.Match(
            profile => Results.Ok(
                mapper.Map<List<StandardProviderResponse>>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> GetProviderById(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid providerId,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
                GetProviderByIdQuery(
                    profileId,
                    providerId
                ), cancellationToken
        );

        return result.Match(
            profile => Results.Ok(
                mapper.Map<ProviderResponse>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> GetStores(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid providerId,
        string? search,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
                GetProviderStoresQuery(
                    profileId,
                    providerId,
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
        Guid providerId,
        Guid storeId,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
                GetProviderStoreByIdQuery(
                    profileId,
                    providerId,
                    storeId
                ), cancellationToken
        );

        return result.Match(
            profile => Results.Ok(
                mapper.Map<StoreResponse>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> GetProviderAdverts(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid providerId,
        CancellationToken cancellationToken)
    {
        var profileId = context.User.GetProfileId();

        var result = await sender.Send(new
                GetProviderAdvertsQuery(
                    profileId,
                    providerId
                ), cancellationToken
        );

        return result.Match(
            profile => Results.Ok(
                mapper.Map<List<AdvertisementResponse>>(result)),
            errors => errors.ToProblemDetails(localizer)
        );
    }
}