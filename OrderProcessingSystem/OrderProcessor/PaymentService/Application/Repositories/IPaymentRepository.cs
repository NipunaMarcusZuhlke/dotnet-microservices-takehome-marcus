using OrderProcessor.PaymentService.Domain;

namespace OrderProcessor.PaymentService.Application.Repositories;

public interface IPaymentRepository
{
    Task SaveProcessedPaymentAsync(Payment payment);

    Task<List<Payment>> GetAllProcessedPaymentsAsync();
}