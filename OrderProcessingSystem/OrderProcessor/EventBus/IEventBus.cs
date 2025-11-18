namespace OrderProcessor.EventBus;

public interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default);
    void Subscribe<TEvent>(IEventHandler<TEvent> handler);
}