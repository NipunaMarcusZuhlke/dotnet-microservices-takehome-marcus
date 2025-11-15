using OrderProcessor.NotificationService.Domain;

namespace OrderProcessor.NotificationService.Application;

public interface INotificationDataStore
{
    void AddNotification(Notification notification);
    List<Notification> GetAllNotifications();
}