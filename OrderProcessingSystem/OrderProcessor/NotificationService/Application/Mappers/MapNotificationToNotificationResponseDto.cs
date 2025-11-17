using OrderProcessor.NotificationService.Application.Dtos;
using OrderProcessor.NotificationService.Domain;

namespace OrderProcessor.NotificationService.Application.Mappers;

public static class MapNotificationToNotificationResponseDto
{
    public static NotificationResponseDto Map(Notification notification) => new NotificationResponseDto(
        notification.NotificationId,
        notification.OrderId,
        notification.PaymentId,
        notification.Amount,
        notification.CustomerEmail,
        notification.PaymentTimestamp);
}