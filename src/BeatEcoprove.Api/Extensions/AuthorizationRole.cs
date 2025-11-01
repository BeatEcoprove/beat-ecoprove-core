using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BeatEcoprove.Api.Extensions;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AuthorizationRole(params string[] roles) : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userType = context.HttpContext.User.GetUserType();

        if (HasRole(userType))
        {
            return;
        }

        context.Result = new UnauthorizedResult();
    }

    private bool HasRole(string userType)
    {
        return roles.Any(role => role == userType);
    }
}