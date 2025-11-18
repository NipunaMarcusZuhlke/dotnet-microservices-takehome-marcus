using OrderProcessor.NotificationService.Application.Dtos;
using OrderProcessor.NotificationService.Application.Mappers;
using OrderProcessor.NotificationService.Domain.Repositories;

namespace OrderProcessor.NotificationService.Application.Services;

public class NotificationsService(INotificationsRepository notificationsRepository) : INotificationsService
{
    public async Task<List<NotificationResponseDto>> GetAllNotificationsAsync()
    {
        var notifications = await notificationsRepository.GetAllNotificationsAsync();
        return notifications
            .Select(notification => notification.MapToNotificationResponseDto())
            .ToList();
    }
}