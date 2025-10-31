namespace BeatEcoprove.Domain.Shared.Models;

public record Key
{
    private const string Delimiter = ":";

    public string Value { get; }

    protected Key(params string[] values)
    {
        Value = string.Join(Delimiter, values);
    }

    public static implicit operator string(Key key) => key.Value;
}