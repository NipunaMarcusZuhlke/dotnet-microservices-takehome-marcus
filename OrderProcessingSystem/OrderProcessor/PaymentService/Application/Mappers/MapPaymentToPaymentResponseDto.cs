using OrderProcessor.PaymentService.Application.Dtos;
using OrderProcessor.PaymentService.Domain;

namespace OrderProcessor.PaymentService.Application.Mappers;

public static class MapPaymentToPaymentResponseDto
{
    public static PaymentResponseDto Map(Payment payment) => new(
        payment.OrderId,
        payment.PaymentId,
        payment.Amount,
        payment.Timestamp);
}
