using OrderProcessor.NotificationService.Application;
using OrderProcessor.NotificationService.Domain;
using OrderProcessor.NotificationService.Infrastructure.Persistance;

namespace OrderProcessor.NotificationService.Infrastructure.Repositories;

public class InMemoryNotificationRepository(INotificationDataStore dataStore): INotificationsRepository
{
    public void SaveNotification(Notification notification)
    {
        dataStore.AddNotification(notification);
    }

    public List<Notification> GetAllNotifications()
    {
        return dataStore.GetAllNotifications();
    }
}