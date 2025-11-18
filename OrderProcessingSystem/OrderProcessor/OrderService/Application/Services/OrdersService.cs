using OrderProcessor.OrderService.Application.Dtos;
using OrderProcessor.OrderService.Application.Mappers;
using OrderProcessor.OrderService.Application.Messaging;
using OrderProcessor.OrderService.Domain;
using OrderProcessor.OrderService.Domain.Repositories;

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
        
        await orderCreatedEventPublisher.PublishAsync(order.MapToOrderCreatedEvent());

        return order.MapToOrderResponseDto();
    }

    public async Task<List<OrderResponseDto>> GetAllOrdersAsync()
    {
        var orders = await orderRepository.GetAllOrdersAsync();
        return orders.Select(order => order.MapToOrderResponseDto()).ToList();
    }

    public async Task<OrderResponseDto?> GetOrderByIdAsync(Guid orderId)
    {
        var order = await orderRepository.GetOrderByIdAsync(orderId);
        return order?.MapToOrderResponseDto();
    }
}