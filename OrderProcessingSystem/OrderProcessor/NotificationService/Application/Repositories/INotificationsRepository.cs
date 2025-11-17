using OrderProcessor.NotificationService.Domain;

namespace OrderProcessor.NotificationService.Application.Repositories;

public interface INotificationsRepository
{
    Task SaveNotificationAsync(Notification notification);

    Task<List<Notification>> GetAllNotificationsAsync();
}