using EventBus;
using OrderProcessor.PaymentService.Application;
using OrderProcessor.SharedDomain.Events;

namespace OrderProcessor.PaymentService.Infrastructure.Event.Publisher;

public class PaymentSucceededEventPublisher(IEventBus eventBus): IPaymentSucceededEventPublisher
{
    public void Publish(PaymentSucceededEvent paymentSucceededEvent)
    {
        eventBus.Publish(paymentSucceededEvent);
    }
}
