using EventBus;
using OrderProcessor.PaymentService.Application;
using OrderProcessor.SharedDomain.Events;

namespace OrderProcessor.PaymentService.Infrastructure.Event.Consumer;

public static class PaymentEventSubscription
{
    public static void Register(IEventBus eventBus, IServiceProvider services)
    {
        eventBus.Subscribe<OrderCreatedEvent>(@event =>
        {
            var service = services.GetRequiredService<IPaymentProcessor>();
            service.ProcessPayment(@event.OrderId, @event.Amount, @event.CustomerEmail);
        });
    }
}