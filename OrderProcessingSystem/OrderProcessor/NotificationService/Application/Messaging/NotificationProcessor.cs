using OrderProcessor.NotificationService.Application.Repositories;
using OrderProcessor.NotificationService.Application.Services;
using OrderProcessor.NotificationService.Domain;

namespace OrderProcessor.NotificationService.Application.Messaging;

public class NotificationProcessor(
    INotificationsRepository notificationsRepository,
    ILogger<NotificationsService> logger) : INotificationProcessor
{
    public async Task ProcessNotificationAsync(Guid orderId, Guid paymentId, decimal amount, string customerEmail,
        DateTime timestamp, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Payment succeeded event receive");

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
        
        // Simulating Notification Processing time.
        await Task.Delay(500, cancellationToken);
        
        logger.LogInformation($"Notification Processed for orderId: {orderId} and paymentId: {paymentId}");

        await notificationsRepository.SaveNotificationAsync(notification);
    }
}