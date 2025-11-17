using EventBus;
using OrderProcessor.PaymentService.Application.Messaging;
using OrderProcessor.SharedDomain.Events;

namespace OrderProcessor.PaymentService.Infrastructure.Event.Consumer;

public class OrderCreatedEventSubscription(IPaymentProcessor paymentProcessor) : IEventHandler<OrderCreatedEvent>
{
    public async Task HandleAsync(OrderCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        await paymentProcessor.ProcessPaymentAsync(@event.OrderId, @event.Amount, @event.CustomerEmail, cancellationToken);
    }
}