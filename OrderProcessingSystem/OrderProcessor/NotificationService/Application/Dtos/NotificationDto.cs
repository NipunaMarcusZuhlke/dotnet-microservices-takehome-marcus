namespace OrderProcessor.NotificationService.Application.Dtos;

public record NotificationDto(Guid NotificationId, Guid OrderId, Guid PaymentId, decimal Amount, string CustomerEmail, DateTime Timestamp);