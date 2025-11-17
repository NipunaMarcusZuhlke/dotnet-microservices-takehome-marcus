using EventBus;
using OrderProcessor.OrderService.Application.Messaging;
using OrderProcessor.SharedDomain.Events;

namespace OrderProcessor.OrderService.Infrastructure.Event;

public class OrderCreatedOrderCreatedEventPublisher(
    IEventBus eventBus,
    ILogger<OrderCreatedOrderCreatedEventPublisher> logger) : IOrderCreatedEventPublisher
{
    public async Task PublishAsync(OrderCreatedEvent orderCreatedEvent)
    {
        try
        {
            await eventBus.PublishAsync(orderCreatedEvent);
            
            logger.LogInformation("Order created event successfully sent for order ID: {OrderId}", orderCreatedEvent.OrderId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Order create event failed to send for order ID: {OrderId}",
                orderCreatedEvent.OrderId);
            throw;
        }
    }
}