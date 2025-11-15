using OrderProcessor.NotificationService.Application;
using OrderProcessor.NotificationService.Domain;

namespace OrderProcessor.NotificationService.Infrastructure.Persistance;

public class InMemoryNotificationDataStore: INotificationDataStore
{
    private static readonly List<Notification> Notifications = [];

    public void AddNotification(Notification notification)
    {
        Notifications.Add(notification);
    }

    public List<Notification> GetAllNotifications()
    {
        return Notifications;
    }
}