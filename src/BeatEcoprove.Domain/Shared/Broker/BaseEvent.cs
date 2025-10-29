using System.Globalization;

namespace BeatEcoprove.Domain.Shared.Broker;

public record Metadata(string Source);

public class BaseEvent<TEvent>(TEvent @event, string eventType) : IBrokerEvent
    where TEvent : IBrokerEvent
{
    public int Version => 1;
    
    public Guid Key => Guid.NewGuid();
    
    public Metadata Metadata => new Metadata("core_service");
    
    public string EventType => eventType;
    
    public TEvent Payload => @event;
    
    public string OccurredAt => DateTime.UtcNow.ToString(
        "yyyy-MM-ddTHH:mm:ss.fffffffK",
        CultureInfo.InvariantCulture
    );
}