using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace BeatEcoprove.Infrastructure.Authentication;

public class JwtReceiver :IConfigurationRetriever<OpenIdConnectConfiguration> 
{
    public async Task<OpenIdConnectConfiguration> GetConfigurationAsync(
        string address, 
        IDocumentRetriever retriever, 
        CancellationToken cancel)
    {
        var doc = await retriever.GetDocumentAsync(address, cancel);
        var jwks = new JsonWebKeySet(doc);
        
        var config = new OpenIdConnectConfiguration
        {
            JsonWebKeySet = jwks,
            JwksUri = address,
        };
        
        foreach (var key in jwks.GetSigningKeys())
        {
            config.SigningKeys.Add(key);
        }

        return config;
    }
}