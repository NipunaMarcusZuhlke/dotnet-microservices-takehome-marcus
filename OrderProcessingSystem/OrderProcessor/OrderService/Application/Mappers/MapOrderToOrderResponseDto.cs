using OrderProcessor.OrderService.Application.Dtos;
using OrderProcessor.OrderService.Domain;

namespace OrderProcessor.OrderService.Application.Mappers;

public static class MapOrderToOrderResponseDto
{
    public static OrderResponseDto Map(Order order) => new(
        order.OrderId,
        order.Amount,
        order.CustomerEmail,
        order.Timestamp);
}