using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;

namespace BeatEcoprove.Infrastructure.Services;

public class ValidationService : IValidationFieldService
{
    private readonly IProfileRepository _profileRepository;
    private readonly Dictionary<string, Func<string, Task<bool>>> _fieldVerifiers;

    public ValidationService(IProfileRepository profileRepository)
    {
        _fieldVerifiers = new Dictionary<string, Func<string, Task<bool>>>
        {
            { "username", IsUserNameAvailableAsync },
        };

        _profileRepository = profileRepository;
    }

    private async Task<bool> IsUserNameAvailableAsync(string validationValue)
    {
        var userName = DisplayName.Create(validationValue);

        return !await _profileRepository.ExistsUserByUserNameAsync(userName.Value, default);
    }

    public Task<bool> IsFieldAvailable(string fieldName, string value)
    {
        if (_fieldVerifiers.TryGetValue(fieldName, out var verifier))
        {
            return verifier(value);
        }

        throw new ArgumentException($"Field {fieldName} is not supported");
    }
}