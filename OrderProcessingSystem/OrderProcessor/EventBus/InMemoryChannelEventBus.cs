using System.Collections.Concurrent;
using System.Threading.Channels;

namespace OrderProcessor.EventBus;

public class InMemoryChannelEventBus(ILogger<InMemoryChannelEventBus> logger) : IEventBus, IDisposable
{
    private readonly ConcurrentDictionary<Type, Channel<object>> _channels = new();
    private readonly ConcurrentDictionary<Type, List<object>> _handlers = new();

    public void Subscribe<TEvent>(IEventHandler<TEvent> handler)
    {
        var eventType = typeof(TEvent);
        
        _handlers.AddOrUpdate(eventType,
            _ => [handler],
            (_, list) =>
            {
                lock (list)
                {
                    list.Add(handler);
                }
                return list;
            });
        
        _channels.GetOrAdd(eventType, t =>
        {
            var channel = Channel.CreateUnbounded<object>();
            _ = Task.Run(() => ProcessLoopAsync<TEvent>(channel, _handlers[eventType], CancellationToken.None));
            return channel;
        });
    }

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
    {
        if (!_channels.TryGetValue(typeof(TEvent), out var channel))
        {
            return;
        }

        if (@event != null) await channel.Writer.WriteAsync(@event, cancellationToken);
    }

    private async Task ProcessLoopAsync<TEvent>(
        Channel<object> channel,
        List<object> handlers,
        CancellationToken token)
    {
        await foreach (var message in channel.Reader.ReadAllAsync(token))
        {
            foreach (var objHandler in handlers)
            {
                var handler = (IEventHandler<TEvent>)objHandler;

                try
                {
                    await handler.HandleAsync((TEvent)message, token);
                }
                catch (Exception ex)
                {
                    logger.LogError($"{typeof(TEvent).Name} handler failed: {ex.Message}");
                }
            }
        }
    }

    public void Dispose()
    {
        foreach (var kv in _channels)
        {
            kv.Value.Writer.Complete();
        }
    }
}