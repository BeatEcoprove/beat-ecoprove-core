namespace BeatEcoprove.Application.Shared.Interfaces.Providers;

public interface IJwtProvider
{
    Task<Dictionary<string, string>> GetClaims(string token);
    Task<bool> ValidateTokenAsync(string token, CancellationToken cancellationToken = default);
    string GenerateRandomCode(int length);
}