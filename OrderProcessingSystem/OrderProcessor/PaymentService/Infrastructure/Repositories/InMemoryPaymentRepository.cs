using OrderProcessor.PaymentService.Application;
using OrderProcessor.PaymentService.Domain;
using OrderProcessor.PaymentService.Infrastructure.Persistance;

namespace OrderProcessor.PaymentService.Infrastructure.Repositories;

public class InMemoryPaymentRepository(IPaymentDataStore paymentDataStore): IPaymentRepository
{
    public void SaveProcessedPayment(Payment payment)
    {
        paymentDataStore.AddPayment(payment);
    }

    public List<Payment> GetAllProcessedPayments()
    {
        return paymentDataStore.GetAllPayments();
    }
}