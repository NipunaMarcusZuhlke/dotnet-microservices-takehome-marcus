using OrderProcessor.NotificationService.Application.Dtos;

namespace OrderProcessor.NotificationService.Application;

public class NotificationsService(INotificationsRepository notificationsRepository) : INotificationsService
{
    public List<NotificationDto> GetAllNotifications()
    {
        var notifications = notificationsRepository.GetAllNotifications();
        return notifications
            .Select(notification => new NotificationDto(
                notification.NotificationId,
                notification.OrderId,
                notification.PaymentId,
                notification.Amount,
                notification.CustomerEmail,
                notification.PaymentTimestamp))
            .ToList();
    }
}