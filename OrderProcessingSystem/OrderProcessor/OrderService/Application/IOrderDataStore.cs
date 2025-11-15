using OrderProcessor.OrderService.Domain;

namespace OrderProcessor.OrderService.Application;

public interface IOrderDataStore
{
    void AddOrder(Order order);
    List<Order> GetAllOrders();
}