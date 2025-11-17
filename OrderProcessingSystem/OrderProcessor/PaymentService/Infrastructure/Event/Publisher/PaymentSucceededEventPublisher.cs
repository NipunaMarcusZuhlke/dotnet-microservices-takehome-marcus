using EventBus;
using OrderProcessor.PaymentService.Application.Messaging;
using OrderProcessor.SharedDomain.Events;

namespace OrderProcessor.PaymentService.Infrastructure.Event.Publisher;

public class PaymentSucceededEventPublisher(IEventBus eventBus, ILogger<PaymentSucceededEventPublisher> logger): IPaymentSucceededEventPublisher
{
    public async Task PublishAsync(PaymentSucceededEvent paymentSucceededEvent)
    {
        try
        {
            await eventBus.PublishAsync(paymentSucceededEvent);
            
            logger.LogInformation("Payment succeeded event successfully sent for order ID: {OrderId} and payment ID: {PaymentId}", paymentSucceededEvent.OrderId, paymentSucceededEvent.PaymentId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Payment succeeded event failed to send for order ID: {OrderId} and payment ID: {PaymentId}", paymentSucceededEvent.OrderId, paymentSucceededEvent.PaymentId);
        }
    }
}
