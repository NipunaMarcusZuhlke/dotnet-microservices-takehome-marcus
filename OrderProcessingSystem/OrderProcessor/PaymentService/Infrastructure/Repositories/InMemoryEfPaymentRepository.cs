using Microsoft.EntityFrameworkCore;
using OrderProcessor.PaymentService.Application.Repositories;
using OrderProcessor.PaymentService.Domain;
using OrderProcessor.PaymentService.Infrastructure.Persistance;

namespace OrderProcessor.PaymentService.Infrastructure.Repositories;

public class InMemoryEfPaymentRepository(PaymentDbContext paymentDbContext, ILogger<InMemoryEfPaymentRepository> logger): IPaymentRepository
{
    public async Task SaveProcessedPaymentAsync(Payment payment)
    {
        try
        {
            await paymentDbContext.Payments.AddAsync(payment);
            await paymentDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to save processed payment for order ID: {OrderId}", payment.OrderId);
            throw;
        }
    }

    public async Task<List<Payment>> GetAllProcessedPaymentsAsync()
    {
        return await paymentDbContext.Payments.ToListAsync();
    }
}