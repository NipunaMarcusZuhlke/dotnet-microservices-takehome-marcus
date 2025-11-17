namespace OrderProcessor.PaymentService.Application.Messaging;

public interface IPaymentProcessor
{ 
    Task ProcessPaymentAsync(Guid orderId, decimal amount, string customerEmail, CancellationToken cancellationToken = default);
}