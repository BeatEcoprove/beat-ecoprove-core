using BeatEcoprove.Infrastructure.Configuration;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace BeatEcoprove.Infrastructure.Authentication;

public static class JwtAuthentication
{
    public static IServiceCollection AddJwks(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                    Env.Jwt.JwksUrl,
                    new JwtReceiver(),
                    new HttpDocumentRetriever
                    {
                        RequireHttps = false,
                    }
                );

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });

        return services;
    }
}