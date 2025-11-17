namespace OrderProcessor.PaymentService.Application.Dtos;

public record PaymentResponseDto(Guid OrderId, Guid PaymentId, decimal Amount, DateTime Timestamp);