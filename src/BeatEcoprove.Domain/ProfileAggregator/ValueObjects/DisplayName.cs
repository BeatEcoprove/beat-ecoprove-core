using BeatEcoprove.Domain.Shared.Errors;
using BeatEcoprove.Domain.Shared.Models;

using ErrorOr;

namespace BeatEcoprove.Domain.ProfileAggregator.ValueObjects;

public class DisplayName : ValueObject
{
    public DisplayName() { }

    private DisplayName(string value) => Value = value;

    public string Value { get; private set; } = null!;

    public static ErrorOr<DisplayName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Errors.Username.InvalidUsername;
        }

        return new DisplayName(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(DisplayName displayName) => displayName.Value;
}