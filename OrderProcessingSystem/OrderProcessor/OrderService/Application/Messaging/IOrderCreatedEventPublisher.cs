using OrderProcessor.SharedDomain.Events;

namespace OrderProcessor.OrderService.Application.Messaging;

public interface IOrderCreatedEventPublisher
{
    Task PublishAsync(OrderCreatedEvent orderCreatedEvent);
}