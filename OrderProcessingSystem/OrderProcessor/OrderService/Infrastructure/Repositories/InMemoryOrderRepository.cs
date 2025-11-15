using OrderProcessor.OrderService.Application;
using OrderProcessor.OrderService.Domain;

namespace OrderProcessor.OrderService.Infrastructure.Repositories;

public class InMemoryOrderRepository(IOrderDataStore orderDataStore) : IOrderRepository
{
    public void CreateOrder(Order order)
    {
        orderDataStore.AddOrder(order);
    }

    public List<Order> GetAllOrders() => orderDataStore.GetAllOrders();
}