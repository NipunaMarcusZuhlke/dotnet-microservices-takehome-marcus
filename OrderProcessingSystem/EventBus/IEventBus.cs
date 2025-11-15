namespace EventBus;

public interface IEventBus
{
    void Publish<TEvent>(TEvent message);
    void Subscribe<TEvent>(Action<TEvent> handler);
}