namespace OrderProcessor.PaymentService.Application.Dtos;

public record PaymentDto(Guid OrderId, Guid PaymentId, decimal Amount, DateTime Timestamp);