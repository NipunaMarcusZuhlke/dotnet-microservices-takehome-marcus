using OrderProcessor.PaymentService.Domain;

namespace OrderProcessor.PaymentService.Application;

public interface IPaymentRepository
{
    void SaveProcessedPayment(Payment payment);

    List<Payment> GetAllProcessedPayments();
}