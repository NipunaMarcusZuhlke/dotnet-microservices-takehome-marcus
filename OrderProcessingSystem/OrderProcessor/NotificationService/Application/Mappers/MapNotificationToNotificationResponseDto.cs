using OrderProcessor.NotificationService.Application.Dtos;
using OrderProcessor.NotificationService.Domain;

namespace OrderProcessor.NotificationService.Application.Mappers;

public static class MapNotificationToNotificationResponseDto
{
    public static NotificationResponseDto MapToNotificationResponseDto(this Notification notification) => new(
        notification.NotificationId,
        notification.OrderId,
        notification.PaymentId,
        notification.Amount,
        notification.CustomerEmail,
        notification.PaymentTimestamp);
}