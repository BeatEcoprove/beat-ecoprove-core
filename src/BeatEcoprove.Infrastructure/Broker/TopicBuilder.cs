using BeatEcoprove.Domain.Shared.Broker;
using BeatEcoprove.Infrastructure.Broker.Serializers;

using MassTransit;

namespace BeatEcoprove.Infrastructure.Broker;

public class TopicBuilder<TEventTopic>(string topic, string topicGroup)
    where TEventTopic : class, IBrokerEvent
{
    private readonly KafkaBrokerDeserializer<TEventTopic> _deserializer = new();
    private readonly KafkaBrokerSerializer<TEventTopic> _serializer = new();

    private readonly List<Type> _consumers = [];

    public TopicBuilder<TEventTopic> WithConsumer<TEvent, TConsumer>()
        where TEvent : class, IBrokerEvent
    {
        _deserializer.Register<TEvent>();
        _consumers.Add(typeof(TConsumer));

        return this;
    }

    public TopicBuilder<TEventTopic> WithEvent<TEvent>()
        where TEvent : class, IBrokerEvent
    {
        _deserializer.Register<TEvent>();

        return this;
    }

    public TopicBuilder<TEventTopic> WithProducer(IRiderRegistrationConfigurator rider)
    {
        rider.AddProducer<TEventTopic>(topic, (ctx, config) =>
        {
            config.SetValueSerializer(_serializer);
        });

        return this;
    }

    public void ApplyConsumers(
        IRegistrationContext context,
        IKafkaFactoryConfigurator configurator)
    {
        configurator.TopicEndpoint<TEventTopic>(topic, topicGroup, kafka =>
        {
            kafka.SetValueDeserializer(_deserializer);

            foreach (var consumer in _consumers)
            {
                kafka.ConfigureConsumer(context, consumer);
            }

            kafka.CreateIfMissing();
        });
    }
}