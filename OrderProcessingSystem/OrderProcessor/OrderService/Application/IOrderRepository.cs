using OrderProcessor.OrderService.Domain;

namespace OrderProcessor.OrderService.Application;

public interface IOrderRepository
{
    void CreateOrder(Order order);
    List<Order> GetAllOrders();
}