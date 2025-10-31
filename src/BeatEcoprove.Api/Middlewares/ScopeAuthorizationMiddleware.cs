namespace BeatEcoprove.Api.Middlewares;

public class ScopeAuthorizationMiddleware(params string[] requiredScopes) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context, 
        EndpointFilterDelegate next)
    {
        var user = context.HttpContext.User;
        var userScopes = user.Claims
            .Where(c => c.Type == "scope")
            .Select(c => c.Value);

        if (!requiredScopes.Any(scope => userScopes.Contains(scope)))
        {
            return Results.Problem(
                statusCode: StatusCodes.Status403Forbidden,
                title: "Forbidden",
                detail: $"Required scopes: {string.Join(", ", requiredScopes)}"
            );
        }

        return await next(context);
    }
}