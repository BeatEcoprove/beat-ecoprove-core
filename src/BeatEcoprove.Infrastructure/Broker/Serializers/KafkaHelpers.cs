using BeatEcoprove.Domain.Shared.Broker;

namespace BeatEcoprove.Infrastructure.Broker.Serializers;

public static class KafkaHelpers
{
    private static string GetEventType(Type type)
    {
        var typeName = type.Name;
        
        if (typeName.EndsWith("Event", StringComparison.Ordinal))
        {
            typeName = typeName[..^5];
        }
        
        return string.Concat(
            typeName.Select((c, i) => 
                i > 0 && char.IsUpper(c) 
                    ? "_" + char.ToLower(c) 
                    : char.ToLower(c).ToString()
            )
        );
    }

    public static string GetEventType(IBrokerEvent @event)
        => GetEventType(@event.GetType());

    public static string GetEventType<TEvent>() where TEvent : IBrokerEvent
        => GetEventType(typeof(TEvent));
}