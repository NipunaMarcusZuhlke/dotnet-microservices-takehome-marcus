namespace OrderProcessor.NotificationService.Application.Messaging;

public interface INotificationProcessor
{
    Task ProcessNotificationAsync(Guid orderId, Guid paymentId, decimal amount, string customerEmail,
        DateTime timestamp, CancellationToken cancellationToken = default);
}