using OrderProcessor.NotificationService.Application.Dtos;

namespace OrderProcessor.NotificationService.Application;

public interface INotificationsService
{
    List<NotificationDto> GetAllNotifications();
}