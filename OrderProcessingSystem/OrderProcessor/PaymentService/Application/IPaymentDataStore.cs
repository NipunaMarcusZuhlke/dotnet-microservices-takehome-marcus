using OrderProcessor.PaymentService.Domain;

namespace OrderProcessor.PaymentService.Application;

public interface IPaymentDataStore
{
    void AddPayment(Payment payment);
    List<Payment> GetAllPayments();
}