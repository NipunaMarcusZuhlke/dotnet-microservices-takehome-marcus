using Microsoft.EntityFrameworkCore;
using OrderProcessor.OrderService.Domain;

namespace OrderProcessor.OrderService.Infrastructure.Persistance;

public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; init; } = default!;
}