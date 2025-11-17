using OrderProcessor.OrderService.Application.Dtos;
using OrderProcessor.OrderService.Application.Mappers;
using OrderProcessor.OrderService.Application.Messaging;
using OrderProcessor.OrderService.Application.Repositories;
using OrderProcessor.OrderService.Domain;

namespace OrderProcessor.OrderService.Application.Services;

public class OrdersService(IOrderRepository orderRepository, IOrderCreatedEventPublisher orderCreatedEventPublisher): IOrdersService
{
    public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderRequestDto createOrderRequestDto)
    {
        var order = new Order
        {
            OrderId = Guid.NewGuid(),
            Timestamp = DateTime.Now,
            Amount = createOrderRequestDto.Amount,
            CustomerEmail = createOrderRequestDto.CustomerEmail
        };
        
        await orderRepository.CreateOrderAsync(order);
        
        await orderCreatedEventPublisher.PublishAsync(MapOrderToOrderCreatedEvent.Map(order));

        return MapOrderToOrderResponseDto.Map(order);
    }

    public async Task<List<OrderResponseDto>> GetAllOrdersAsync()
    {
        var orders = await orderRepository.GetAllOrdersAsync();
        return orders.Select(MapOrderToOrderResponseDto.Map).ToList();
    }

    public async Task<OrderResponseDto?> GetOrderByIdAsync(Guid orderId)
    {
        var order = await orderRepository.GetOrderByIdAsync(orderId);
        return order == null ? null : MapOrderToOrderResponseDto.Map(order);
    }
}