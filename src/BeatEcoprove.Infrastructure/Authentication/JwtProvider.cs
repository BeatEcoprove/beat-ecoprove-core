using System.IdentityModel.Tokens.Jwt;
using System.Text;

using BeatEcoprove.Application.Shared.Interfaces.Providers;
using BeatEcoprove.Infrastructure.Configuration;

using Microsoft.IdentityModel.Tokens;

namespace BeatEcoprove.Infrastructure.Authentication;

public class JwtProvider : IJwtProvider
{
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

    public JwtProvider()
    {
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    }

    public async Task<Dictionary<string, string>> GetClaims(string token)
    {
        if (!await ValidateTokenAsync(token))
        {
            throw new SecurityTokenException();
        }

        return _jwtSecurityTokenHandler
            .ReadJwtToken(token)
            .Claims
            .ToDictionary(claim => claim.Type, claim => claim.Value);
    }

    public async Task<bool> ValidateTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        var result = await _jwtSecurityTokenHandler.ValidateTokenAsync(
            token,
            GetTokenValidationParameters());

        return result.IsValid;
    }

    public string GenerateRandomCode(int length)
    {
        var random = new Random();
        var minValue = (int)Math.Pow(10, length - 1);
        var maxValue = (int)Math.Pow(10, length) - 1;
        var code = random.Next(minValue, maxValue + 1);

        return code.ToString("D" + length);
    }

    private static TokenValidationParameters GetTokenValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = Env.Jwt.JwtIssuer,
            ValidateAudience = true,
            ValidAudience = Env.Jwt.JwtAudience,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Env.Jwt.SecretKey)),
            ValidateIssuerSigningKey = true
        };
    }
}