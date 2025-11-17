using OrderProcessor.SharedDomain.Events;

namespace OrderProcessor.PaymentService.Application.Messaging;

public interface IPaymentSucceededEventPublisher
{
    Task PublishAsync(PaymentSucceededEvent paymentSucceededEvent);
}