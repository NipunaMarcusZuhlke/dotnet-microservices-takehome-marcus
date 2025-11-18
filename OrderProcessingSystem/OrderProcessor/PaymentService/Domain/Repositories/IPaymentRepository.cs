namespace OrderProcessor.PaymentService.Domain.Repositories;

public interface IPaymentRepository
{
    Task SaveProcessedPaymentAsync(Payment payment);

    Task<List<Payment>> GetAllProcessedPaymentsAsync();
}