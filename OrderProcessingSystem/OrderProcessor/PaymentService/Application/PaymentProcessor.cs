using OrderProcessor.PaymentService.Domain;
using OrderProcessor.SharedDomain.Events;

namespace OrderProcessor.PaymentService.Application;

public class PaymentProcessor(
    IPaymentRepository paymentRepository,
    IPaymentSucceededEventPublisher paymentSucceededEventPublisher) : IPaymentProcessor
{
    public void ProcessPayment(Guid orderId, decimal amount, string customerEmail)
    {
        var payment = new Payment
        {
            OrderId = orderId,
            Amount = amount,
            CustomerEmail = customerEmail,
            PaymentId = Guid.NewGuid(),
            Timestamp = DateTime.Now,
        };

        paymentRepository.SaveProcessedPayment(payment);

        paymentSucceededEventPublisher.Publish(new PaymentSucceededEvent(
            payment.OrderId,
            payment.PaymentId,
            payment.Amount,
            payment.CustomerEmail,
            payment.Timestamp));
    }
}