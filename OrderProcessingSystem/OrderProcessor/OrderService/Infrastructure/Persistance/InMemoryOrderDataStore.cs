using OrderProcessor.OrderService.Application;
using OrderProcessor.OrderService.Domain;

namespace OrderProcessor.OrderService.Infrastructure.Persistance;

public class InMemoryOrderDataStore: IOrderDataStore
{
    private static readonly List<Order> Orders = [];

    public void AddOrder(Order order)
    {
        Orders.Add(order);
    }

    public List<Order> GetAllOrders()
    {
        return Orders;
    }
}