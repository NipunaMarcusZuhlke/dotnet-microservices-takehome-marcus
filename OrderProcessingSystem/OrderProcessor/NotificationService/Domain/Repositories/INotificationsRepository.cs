namespace OrderProcessor.NotificationService.Domain.Repositories;

public interface INotificationsRepository
{
    Task SaveNotificationAsync(Notification notification);

    Task<List<Notification>> GetAllNotificationsAsync();
}