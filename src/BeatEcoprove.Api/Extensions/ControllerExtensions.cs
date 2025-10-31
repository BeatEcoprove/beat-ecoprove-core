using BeatEcoprove.Api.Middlewares;
using BeatEcoprove.Application.Shared.Multilanguage;

using ErrorOr;

namespace BeatEcoprove.Api.Extensions;

public static class ControllerExtensions
{
    public static RouteHandlerBuilder RequireScopes(
        this RouteHandlerBuilder builder, 
        params string[] scopes)
    {
        return builder.AddEndpointFilter(new ScopeAuthorizationMiddleware(scopes));
    }
    
    public static IResult ToProblemDetails(this List<Error> errors, ILanguageCulture localizer)
    {
        if (errors.Count == 0)
        {
            return Results.Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An error occurred");
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            var validationErrors = errors.ToDictionary(
                error => error.Code,
                error => new[] { localizer.GetChunk(error.Code, error.Description) }
            );

            return Results.ValidationProblem(
                validationErrors,
                title: "One or more validation errors occurred");
        }

        var firstError = errors[0];
        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };

        return Results.Problem(
            statusCode: statusCode,
            title: localizer.GetChunk(firstError.Code, firstError.Description));
    }
}