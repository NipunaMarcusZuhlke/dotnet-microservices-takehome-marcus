namespace OrderProcessor.NotificationService.Application;

public interface INotificationProcessor
{
    void ProcessNotification(Guid orderId, Guid paymentId, decimal amount, string customerEmail, DateTime timestamp);
}