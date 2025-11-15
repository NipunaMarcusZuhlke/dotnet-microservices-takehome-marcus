using OrderProcessor.PaymentService.Application;
using OrderProcessor.PaymentService.Domain;

namespace OrderProcessor.PaymentService.Infrastructure.Persistance;

public class InMemoryPaymentDataStore: IPaymentDataStore
{
    private static readonly List<Payment> Payments = [];

    public void AddPayment(Payment payment)
    {
        Payments.Add(payment);
    }

    public List<Payment> GetAllPayments()
    {
        return Payments;
    }
}