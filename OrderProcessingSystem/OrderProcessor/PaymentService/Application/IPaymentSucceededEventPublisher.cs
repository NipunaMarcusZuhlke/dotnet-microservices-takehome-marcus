using OrderProcessor.SharedDomain.Events;

namespace OrderProcessor.PaymentService.Application;

public interface IPaymentSucceededEventPublisher
{
    void Publish(PaymentSucceededEvent paymentSucceededEvent);
}