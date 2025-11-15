using EventBus;
using OrderProcessor.OrderService.Application;
using OrderProcessor.SharedDomain.Events;

namespace OrderProcessor.OrderService.Infrastructure.Event;

public class OrderCreatedOrderCreatedEventPublisher(IEventBus eventBus): IOrderCreatedEventPublisher
{
    public void Publish(OrderCreatedEvent orderCreatedEvent)
    {
        eventBus.Publish(orderCreatedEvent);
    }
}
