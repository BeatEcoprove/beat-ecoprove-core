using System.Text.Json;
using System.Text.Json.Serialization;

using BeatEcoprove.Domain.Shared.Broker;

using Confluent.Kafka;

namespace BeatEcoprove.Infrastructure.Broker.Serializers;

public class KafkaBrokerSerializer : ISerializer<IAuthEvent>
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        PropertyNameCaseInsensitive = true,
        WriteIndented = false,
        DefaultIgnoreCondition = JsonIgnoreCondition.Never
    };

    public byte[] Serialize(IAuthEvent data, SerializationContext context)
    {
        var @event = new BaseEvent<IAuthEvent>(
            data,
            KafkaHelpers.GetEventType(data)
        );
        
        return JsonSerializer.SerializeToUtf8Bytes(
            @event,
            @event.GetType(),
            _options
        );
    }
}