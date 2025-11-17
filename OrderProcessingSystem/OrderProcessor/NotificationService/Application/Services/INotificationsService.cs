using OrderProcessor.NotificationService.Application.Dtos;

namespace OrderProcessor.NotificationService.Application.Services;

public interface INotificationsService
{
    Task<List<NotificationResponseDto>> GetAllNotificationsAsync();
}