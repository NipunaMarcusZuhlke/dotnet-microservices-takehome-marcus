namespace OrderProcessor.PaymentService.Application;

public interface IPaymentProcessor
{ 
    void ProcessPayment(Guid orderId, decimal amount, string customerEmail);
}