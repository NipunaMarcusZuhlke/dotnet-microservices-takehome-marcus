using OrderProcessor.PaymentService.Application.Mappers;
using OrderProcessor.PaymentService.Application.Repositories;
using OrderProcessor.PaymentService.Domain;

namespace OrderProcessor.PaymentService.Application.Messaging;

public class PaymentProcessor(
    IPaymentRepository paymentRepository,
    IPaymentSucceededEventPublisher paymentSucceededEventPublisher,
    ILogger<PaymentProcessor> logger) : IPaymentProcessor
{
    public async Task ProcessPaymentAsync(Guid orderId, decimal amount, string customerEmail, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Order received with orderId: {orderId} for payment", orderId);

        var payment = new Payment
        {
            OrderId = orderId,
            Amount = amount,
            CustomerEmail = customerEmail,
            PaymentId = Guid.NewGuid(),
            Timestamp = DateTime.Now,
        };
        
        // Simulating Payment Processing time.
        await Task.Delay(500, cancellationToken);

        await paymentRepository.SaveProcessedPaymentAsync(payment);

        logger.LogInformation("Successfully processed payment: {PaymentId} for orderId: {orderId}", payment.PaymentId,
            orderId);

        await paymentSucceededEventPublisher.PublishAsync(MapPaymentToPaymentSucceededEvent.Map(payment));
    }
}