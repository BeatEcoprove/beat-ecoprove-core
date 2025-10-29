using System.Globalization;
using System.Text.Json.Serialization;

namespace BeatEcoprove.Domain.Shared.Broker;

public record Metadata(string Source);

public class BaseEvent<TEvent>(TEvent @event, string eventType) : IBrokerEvent
    where TEvent : IBrokerEvent
{
    public int Version => 1;
    
    public Guid Key { get; init; } = Guid.NewGuid();

    public Metadata Metadata { get; init; } = new("core_service");

    public string EventType { get; init; } = eventType;

    public object Payload { get; init; } = @event;

    public string OccurredAt { get; init; } = DateTime.UtcNow.ToString(
        "yyyy-MM-ddTHH:mm:ss.fffffffK",
        CultureInfo.InvariantCulture
    );
}
