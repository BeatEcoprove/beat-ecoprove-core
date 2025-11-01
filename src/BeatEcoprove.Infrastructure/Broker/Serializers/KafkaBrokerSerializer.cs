using System.Text.Json;
using System.Text.Json.Serialization;

using BeatEcoprove.Domain.Shared.Broker;

using Confluent.Kafka;

namespace BeatEcoprove.Infrastructure.Broker.Serializers;

public class KafkaBrokerSerializer<TEvent> : ISerializer<TEvent>
    where TEvent : class, IBrokerEvent
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        PropertyNameCaseInsensitive = true,
        WriteIndented = false,
        IncludeFields = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.Never
    };

    public byte[] Serialize(TEvent data, SerializationContext context)
    {
        var @event = new BaseEvent<TEvent>(
            data,
            KafkaHelpers.GetEventType(data)
        );

        return JsonSerializer.SerializeToUtf8Bytes(
            @event,
            _options
        );
    }
}