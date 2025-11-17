using OrderProcessor.PaymentService.Domain;
using OrderProcessor.SharedDomain.Events;

namespace OrderProcessor.PaymentService.Application.Mappers;

public static class MapPaymentToPaymentSucceededEvent
{
    public static PaymentSucceededEvent Map(Payment payment) => new(
        payment.OrderId,
        payment.PaymentId,
        payment.Amount,
        payment.CustomerEmail,
        payment.Timestamp);
}