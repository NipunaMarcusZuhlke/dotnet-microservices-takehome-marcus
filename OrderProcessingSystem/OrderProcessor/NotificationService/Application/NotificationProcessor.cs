using OrderProcessor.NotificationService.Domain;

namespace OrderProcessor.NotificationService.Application;

public class NotificationProcessor(INotificationsRepository notificationsRepository) : INotificationProcessor
{
    public void ProcessNotification(Guid orderId, Guid paymentId, decimal amount, string customerEmail,
        DateTime timestamp)
    {
        var notification = new Notification
        {
            NotificationId = Guid.NewGuid(),
            OrderId = orderId,
            PaymentId = paymentId,
            Amount = amount,
            CustomerEmail = customerEmail,
            PaymentTimestamp = timestamp,
            NotificationTimestamp = DateTime.Now
        };

        notificationsRepository.SaveNotification(notification);
    }
}