using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Domain.Shared.Models;

using StackExchange.Redis;

namespace BeatEcoprove.Infrastructure.Persistence.Repositories;

public class KeyValueRepository(IDatabase database) : IKeyValueRepository<string>
{
    public async Task AddAsync(Key key, string value, TimeSpan? expirationSpan = null)
    {
        ArgumentNullException.ThrowIfNull(value);

        await database.StringAppendAsync(key.Value, value.ToString());
        await database.KeyExpireAsync(key.Value, expirationSpan ?? TimeSpan.FromDays(7));
    }

    public async Task DeleteAsync(Key key)
    {
        await database.StringGetDeleteAsync(key.Value);
    }

    public async Task<string?> GetAndDeleteAsync(Key key)
    {
        return await database.StringGetDeleteAsync(key.Value);
    }

    public async Task<string?> GetAsync(Key key)
    {
        return await database.StringGetAsync(key.Value);
    }
}