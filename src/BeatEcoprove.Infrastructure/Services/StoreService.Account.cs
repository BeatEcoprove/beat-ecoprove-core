using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;

using ErrorOr;

namespace BeatEcoprove.Infrastructure.Services;

public partial class StoreService
{
    private Task<ErrorOr<AuthId>> CreateAccount(
        string email,
        string password,
        Profile profile,
        Stream stream,
        CancellationToken cancellationToken)
    {
        var authId = AuthId.Create(Guid.NewGuid());
        profile.SetAuthId(authId);
        return Task.FromResult<ErrorOr<AuthId>>(authId);
    }
}