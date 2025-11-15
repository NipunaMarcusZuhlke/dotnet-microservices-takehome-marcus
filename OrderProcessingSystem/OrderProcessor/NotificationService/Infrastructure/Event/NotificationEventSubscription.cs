using EventBus;
using OrderProcessor.NotificationService.Application;
using OrderProcessor.SharedDomain.Events;

namespace OrderProcessor.NotificationService.Infrastructure.Event;

public static class NotificationEventSubscription
{
    public static void Register(IEventBus eventBus, IServiceProvider services)
    {
        eventBus.Subscribe<PaymentSucceededEvent>(@event =>
        {
            var service = services.GetRequiredService<INotificationProcessor>();
            service.ProcessNotification(
                @event.OrderId,
                @event.PaymentId,
                @event.Amount,
                @event.CustomerEmail,
                @event.TimeStamp);
        });
    }
}