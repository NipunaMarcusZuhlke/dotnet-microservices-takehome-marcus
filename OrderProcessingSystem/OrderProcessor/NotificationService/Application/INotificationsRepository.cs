using OrderProcessor.NotificationService.Domain;

namespace OrderProcessor.NotificationService.Application;

public interface INotificationsRepository
{
    void SaveNotification(Notification notification);

    List<Notification> GetAllNotifications();
}