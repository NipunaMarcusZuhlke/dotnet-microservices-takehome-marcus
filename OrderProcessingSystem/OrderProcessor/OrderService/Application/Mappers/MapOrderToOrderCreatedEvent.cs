using OrderProcessor.OrderService.Domain;
using OrderProcessor.SharedDomain.Events;

namespace OrderProcessor.OrderService.Application.Mappers;

public static class MapOrderToOrderCreatedEvent
{
    public static OrderCreatedEvent Map(Order order) => new(
        order.OrderId,
        order.Amount,
        order.CustomerEmail);
}