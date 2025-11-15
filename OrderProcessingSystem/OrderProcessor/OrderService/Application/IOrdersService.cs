using OrderProcessor.OrderService.Application.Dtos;

namespace OrderProcessor.OrderService.Application;

public interface IOrdersService
{ 
    void CreateOrder(CreateOrderDto createOrderDto);
    List<OrderDto> GetAllOrders();
}