using OrderProcessor.OrderService.Application.Dtos;
using OrderProcessor.OrderService.Domain;

namespace OrderProcessor.OrderService.Application.Mappers;

public static class MapOrderToOrderResponseDto
{
    public static OrderResponseDto MapToOrderResponseDto(this Order order) => new(
        order.OrderId,
        order.Amount,
        order.CustomerEmail,
        order.Timestamp);
}