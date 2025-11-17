namespace OrderProcessor.NotificationService.Application.Dtos;

public record NotificationResponseDto(Guid NotificationId, Guid OrderId, Guid PaymentId, decimal Amount, string CustomerEmail, DateTime Timestamp);