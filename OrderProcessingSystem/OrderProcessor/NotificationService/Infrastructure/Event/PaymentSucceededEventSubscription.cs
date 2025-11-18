using OrderProcessor.EventBus;
using OrderProcessor.NotificationService.Application.Messaging;
using OrderProcessor.SharedDomain.Events;

namespace OrderProcessor.NotificationService.Infrastructure.Event;

public class PaymentSucceededEventSubscription(INotificationProcessor notificationProcessor): IEventHandler<PaymentSucceededEvent>
{
    public async Task HandleAsync(PaymentSucceededEvent @event, CancellationToken cancellationToken = default)
    {
        await notificationProcessor.ProcessNotificationAsync(
            @event.OrderId,
            @event.PaymentId,
            @event.Amount,
            @event.CustomerEmail,
            @event.TimeStamp,
            cancellationToken);
    }
}