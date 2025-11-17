using Microsoft.EntityFrameworkCore;
using OrderProcessor.PaymentService.Domain;

namespace OrderProcessor.PaymentService.Infrastructure.Persistance;

public class PaymentDbContext(DbContextOptions<PaymentDbContext> options) : DbContext(options)
{
    public DbSet<Payment> Payments { get; init; } = default!;
}