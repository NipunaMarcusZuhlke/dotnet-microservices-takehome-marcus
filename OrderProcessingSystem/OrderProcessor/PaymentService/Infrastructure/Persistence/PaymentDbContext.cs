using Microsoft.EntityFrameworkCore;
using OrderProcessor.PaymentService.Domain;

namespace OrderProcessor.PaymentService.Infrastructure.Persistence;

public class PaymentDbContext(DbContextOptions<PaymentDbContext> options) : DbContext(options)
{
    public DbSet<Payment> Payments { get; init; } = default!;
}