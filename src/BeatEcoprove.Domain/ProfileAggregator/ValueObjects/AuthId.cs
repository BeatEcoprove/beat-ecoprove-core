using BeatEcoprove.Domain.Shared.ValueObjects;

namespace BeatEcoprove.Domain.ProfileAggregator.ValueObjects;

public sealed class AuthId : Id<Guid>
{
    private AuthId(Guid value) : base(value)
    {
    }

    public static AuthId Create(Guid value)
    {
        return new AuthId(value);
    }
}