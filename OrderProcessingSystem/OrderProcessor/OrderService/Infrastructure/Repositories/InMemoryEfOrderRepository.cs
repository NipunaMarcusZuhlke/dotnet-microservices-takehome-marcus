using Microsoft.EntityFrameworkCore;
using OrderProcessor.OrderService.Domain;
using OrderProcessor.OrderService.Domain.Repositories;
using OrderProcessor.OrderService.Infrastructure.Persistence;

namespace OrderProcessor.OrderService.Infrastructure.Repositories;

public class InMemoryEfOrderRepository(OrderDbContext orderDbContext, ILogger<InMemoryEfOrderRepository> logger)
    : IOrderRepository
{
    public async Task CreateOrderAsync(Order order)
    {
        try
        {
            await orderDbContext.Orders.AddAsync(order);
            await orderDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create order for customer: {CustomerEmail}", order.CustomerEmail);
            throw;
        }
    }

    public async Task<List<Order>> GetAllOrdersAsync() => await orderDbContext.Orders.ToListAsync();

    public async Task<Order?> GetOrderByIdAsync(Guid orderId) => await orderDbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
}