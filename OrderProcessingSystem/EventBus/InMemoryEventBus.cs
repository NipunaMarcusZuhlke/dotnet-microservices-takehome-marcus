namespace EventBus;

public class InMemoryEventBus : IEventBus
{
    private readonly Dictionary<Type, List<Delegate>> _handlers = new();

    public void Publish<TEvent>(TEvent message)
    {
        var eventType = typeof(TEvent);
        if (!_handlers.ContainsKey(eventType)) return;

        foreach (var handler in _handlers[eventType])
        {
            var action = (Action<TEvent>)handler;
            action(message);
        }
    }

    public void Subscribe<TEvent>(Action<TEvent> handler)
    {
        var eventType = typeof(TEvent);
        
        if (!_handlers.ContainsKey(eventType))
        {
            _handlers[eventType] = [];
        }
        
        _handlers[eventType].Add(handler);
    }
}