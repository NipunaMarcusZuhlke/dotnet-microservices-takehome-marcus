using OrderProcessor.OrderService.Application.Dtos;

namespace OrderProcessor.OrderService.Application.Services;

public interface IOrdersService
{ 
    Task<OrderResponseDto> CreateOrderAsync(CreateOrderRequestDto createOrderRequestDto);
    Task<List<OrderResponseDto>> GetAllOrdersAsync();
    Task<OrderResponseDto?> GetOrderByIdAsync(Guid orderId);
}