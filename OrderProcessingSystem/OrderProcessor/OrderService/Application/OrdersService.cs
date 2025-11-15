using OrderProcessor.OrderService.Application.Dtos;
using OrderProcessor.OrderService.Domain;
using OrderProcessor.SharedDomain.Events;

namespace OrderProcessor.OrderService.Application;

public class OrdersService(IOrderRepository orderRepository, IOrderCreatedEventPublisher orderCreatedEventPublisher): IOrdersService
{
    public void CreateOrder(CreateOrderDto createOrderDto)
    {
        var order = new Order
        {
            OrderId = Guid.NewGuid(),
            Timestamp = DateTime.Now,
            Amount = createOrderDto.Amount,
            CustomerEmail = createOrderDto.CustomerEmail
        };
        
        orderRepository.CreateOrder(order);
        
        orderCreatedEventPublisher.Publish(new OrderCreatedEvent(order.OrderId, order.Amount, order.CustomerEmail));
    }

    public List<OrderDto> GetAllOrders()
    {
        var orders = orderRepository.GetAllOrders();
        return orders.Select(order => new OrderDto(
            order.OrderId,
            order.Amount,
            order.CustomerEmail,
            order.Timestamp)).ToList();
    }
}