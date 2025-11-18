namespace OrderProcessor.OrderService.Domain.Repositories;

public interface IOrderRepository
{
    Task CreateOrderAsync(Order order);
    Task<List<Order>> GetAllOrdersAsync();
    Task<Order?> GetOrderByIdAsync(Guid orderId);
}