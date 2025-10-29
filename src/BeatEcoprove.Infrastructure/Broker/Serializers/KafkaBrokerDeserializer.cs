using System.Text;
using System.Text.Json;

using BeatEcoprove.Domain.Shared.Broker;

using Confluent.Kafka;

namespace BeatEcoprove.Infrastructure.Broker.Serializers;

public class KafkaBrokerDeserializer<IEvent> : IDeserializer<IEvent>
    where IEvent : class, IBrokerEvent
{
    private readonly Dictionary<string, Type> _eventTypes = new();
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        PropertyNameCaseInsensitive = true,
    };

    public void Register<TEvent>(string eventType) where TEvent : IBrokerEvent
    {
        _eventTypes[eventType] = typeof(TEvent);
    }
    
    public void Register<TEvent>() where TEvent : IBrokerEvent
    {
        var eventType = KafkaHelpers.GetEventType<TEvent>();
        _eventTypes[eventType] = typeof(TEvent);
    }

    public IEvent Deserialize(
        ReadOnlySpan<byte> data,
        bool isNull, 
        SerializationContext context)
    {
        if (isNull) return null!;

        var json = Encoding.UTF8.GetString(data);
        var doc = JsonDocument.Parse(json);
        
        if (!doc.RootElement.TryGetProperty("event_type", out var eventTypeElement) 
            || !doc.RootElement.TryGetProperty("payload", out var payloadElement))
            return null!;

        var eventType = eventTypeElement.GetString();

        if (eventType == null)
            return null!;

        if (_eventTypes.TryGetValue(eventType, out var type))
        {
            return (IEvent)payloadElement.Deserialize(type, _options)!;
        }

        return null!;
    }
}
