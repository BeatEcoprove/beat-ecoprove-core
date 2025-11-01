using System.Net;
using System.Text.Json;

using BeatEcoprove.Application.Shared.Helpers;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Providers;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Errors;

using ErrorOr;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BeatEcoprove.Api.Middlewares;

public class ProfileCheckerMiddleware(
    IJwtProvider jwtProvider,
    IProfileRepository profileRepository)
    : IMiddleware
{
    private const string ProfileIdKey = "profileId";

    private async Task<ErrorOr<bool>> IsProfileValid(Guid authId, ProfileId profileId, CancellationToken cancellationToken = default)
    {
        var profile = await profileRepository.GetByIdAsync(profileId, cancellationToken);
        if (profile == null) return Errors.User.ProfileDoesNotExists;

        return profile.AuthId.Value == authId;
    }

    private async Task<ErrorOr<(Guid AuthId, string Email)>> GetClaimsFromToken(HttpContext context)
    {
        var authHeader = context.Request.Headers.Authorization.ToString();
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            return Errors.Token.MissingToken;
        }

        var accessToken = authHeader.Split(" ")[1];
        IDictionary<string, string> claims;

        try
        {
            claims = await jwtProvider.GetClaims(accessToken);
        }
        catch (SecurityTokenException)
        {
            return Errors.Token.InvalidToken;
        }

        if (!claims.TryGetValue(UserClaims.AccountId, out var authId)
            || !Guid.TryParse(authId, out var parsedAuthId)
            || !claims.TryGetValue(UserClaims.Email, out var email))
        {
            return Errors.Token.InvalidToken;
        }

        return (parsedAuthId, email);
    }

    private static Task ReturnUnAuthorized(HttpContext context, Error error)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

        var response = JsonSerializer.Serialize(
            new ProblemDetails
            {
                Status = context.Response.StatusCode,
                Title = error.Description,
                Type = error.Code
            });

        return context.Response.WriteAsync(response);
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var profileIdValue = context.Request.RouteValues[ProfileIdKey]?.ToString();

        if (string.IsNullOrWhiteSpace(profileIdValue) || !Guid.TryParse(profileIdValue, out _))
        {
            await next(context);
            return;
        }

        var claims = await GetClaimsFromToken(context);

        if (claims.IsError)
        {
            await ReturnUnAuthorized(context, claims.FirstError);
            return;
        }

        var (authId, email) = claims.Value;
        var isProfileValid = await IsProfileValid(authId, ProfileId.Create(Guid.Parse(profileIdValue!)));

        if (isProfileValid.IsError)
        {
            await ReturnUnAuthorized(context, isProfileValid.FirstError);
            return;
        }

        if (!isProfileValid.Value)
        {
            await ReturnUnAuthorized(context, Errors.User.ProfileDoesNotBelongToAuth);
            return;
        }

        await next(context);
    }

}