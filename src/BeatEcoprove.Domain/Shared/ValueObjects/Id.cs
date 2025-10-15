namespace BeatEcoprove.Domain.Shared.ValueObjects;

public abstract class Id<T>
{
    public T Value { get; }

    protected Id(T value)
    {
        Value = value;
    }
}