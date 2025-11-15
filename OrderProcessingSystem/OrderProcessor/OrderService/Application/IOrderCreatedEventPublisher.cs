using OrderProcessor.SharedDomain.Events;

namespace OrderProcessor.OrderService.Application;

public interface IOrderCreatedEventPublisher
{
    void Publish(OrderCreatedEvent orderCreatedEvent);
}