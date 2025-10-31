using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.Profiles.Commands.CreateProfile;
using BeatEcoprove.Application.Profiles.Commands.DeleteNestedProfile;
using BeatEcoprove.Application.Profiles.Commands.UpdateProfile;
using BeatEcoprove.Application.Profiles.Queries.GetMyProfiles;
using BeatEcoprove.Application.Profiles.Queries.GetProfile;
using BeatEcoprove.Application.Shared.Inputs;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Contracts.Profile;

using MapsterMapper;

using MediatR;

namespace BeatEcoprove.Api.Controllers.V1;

public class ProfileController() : ApiCarterModule()
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var profiles = CreateVersionedGroup(app, "profiles")
                .RequireAuthorization();

        profiles.MapPost("/client", CreateClientProfile)
            .RequireScopes("profile:create");
        
        profiles.MapPost("/organization", CreateOrganizationProfile)
            .RequireScopes("profile:create");

        profiles.MapGet("/me", GetMe)
            .RequireScopes("profile:view");
        
        profiles.MapGet(String.Empty, GetProfiles)
            .RequireScopes("profile:view");
        
        // TODO: did not test this one
        profiles.MapDelete("{id:guid}", DeleteProfile)
            .RequireScopes("profile:delete");
        
        // TODO: this failed
        profiles.MapPut("{id:guid}", UpdateProfile)
            .RequireScopes("profile:update");
    }

    private static bool VerifyProfileId(HttpContext context, Guid profileId)
        => context.User.GetProfileId() == profileId;

    private static async Task<IResult> CreateClientProfile(
        ISender sender, 
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        CreateClientProfileRequest request,
        CancellationToken cancellationToken) {

        if (!VerifyProfileId(context, request.ProfileId))
        {
            return Results.Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: "Forbidden",
                detail: "Don't have access to this profile account"
            );
        }

        var result= await sender
            .Send(new CreateConsumerCommand(
                request.ProfileId,
                new PersonalInfoInput(
                    request.FirstName,
                    request.LastName,
                    request.DisplayName,
                    request.BirthDate,
                    request.Gender,
                    request.Biography,
                    request.PhoneNumber
                )
            ), cancellationToken);
        
        return result.Match(
            profile => Results.Created(
                $"/v1/profiles/{result.Value.Id}", 
                mapper.Map<ProfileResponse>(profile)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> CreateOrganizationProfile(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        CreateOrganizationRequest request,
        CancellationToken cancellationToken)
    {
        if (!VerifyProfileId(context, request.ProfileId))
        {
            return Results.Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: "Forbidden",
                detail: "Don't have access to this profile account"
            );
        }

        var result = await sender
            .Send(new CreateOrganizationCommand(
                request.ProfileId,
                new OrganizationInfoInput(
                    request.FirstName,
                    request.LastName,
                    request.DisplayName,
                    request.Biography,
                    request.PhoneNumber,
                    new AddressInfoInput(
                        request.Street,
                        request.Port,
                        request.Locality,
                        request.ZipCode
                    )
                )
            ), cancellationToken);

        return result.Match(
            profile => Results.Created(
                $"/v1/profiles/{result.Value.Id}",
                mapper.Map<ProfileResponse>(profile)),
            errors => errors.ToProblemDetails(localizer)
        );
    }

    private static async Task<IResult> GetMe(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        CancellationToken cancellationToken)
    {
        var result = await sender
            .Send(new GetProfileQuery(context.User.GetProfileId()), 
                cancellationToken);

        return result.Match(
            profile => Results.Ok(
                mapper.Map<ProfileResponse>(profile)),
            errors => errors.ToProblemDetails(localizer)
        );
    }
    
    private static async Task<IResult> GetProfiles(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        CancellationToken cancellationToken)
    {
        var result = await sender
            .Send(new GetMyProfilesQuery(), cancellationToken);

        return result.Match(
            profile => Results.Ok(
                mapper.Map<ProfileResponse>(profile)),
            errors => errors.ToProblemDetails(localizer)
        );
    }
    
    private static async Task<IResult> DeleteProfile(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid id,
        CancellationToken cancellationToken)
    {
        if (VerifyProfileId(context, id))
        {
            return Results.Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Bad Request",
                detail: "Is not possible to delete the profile that it's being used"
            );
        }
        
        var result = await sender
            .Send(new DeleteNestedProfileCommand(id), 
                cancellationToken);

        return result.Match(
            profile => Results.Ok(
                mapper.Map<ProfileResponse>(profile)),
            errors => errors.ToProblemDetails(localizer)
        );
    }
    
    private static async Task<IResult> UpdateProfile(
        ISender sender,
        IMapper mapper,
        ILanguageCulture localizer,
        HttpContext context,
        Guid id,
        UpdateProfileRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender
            .Send(new UpdateProfileCommand(
                id,
                new UpdateInput(
                       request.FirstName,
                       request.LastName,
                       request.DisplayName,
                       request.Gender,
                       request.PhoneNumber
                    )
                ),
                cancellationToken
            );

        return result.Match(
            profile => Results.Ok(
                mapper.Map<ProfileResponse>(profile)),
            errors => errors.ToProblemDetails(localizer)
        );
    }
} 