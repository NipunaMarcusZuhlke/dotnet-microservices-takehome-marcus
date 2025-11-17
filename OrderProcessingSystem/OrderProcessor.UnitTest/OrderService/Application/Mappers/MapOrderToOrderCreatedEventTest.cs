using OrderProcessor.OrderService.Application.Mappers;
using OrderProcessor.OrderService.Domain;
using Shouldly;

namespace OrderProcessor.UnitTest.OrderService.Application.Mappers;

public class MapOrderToOrderCreatedEventTest
{
    [Fact]
    public void GivenOrder_WhenMapToOrderCreatedEvent_ReturnOrderCreatedEvent()
    {
        var orderId = Guid.NewGuid();
        var order = new Order
        {
            OrderId = orderId,
            Amount = (decimal)89.09, 
            CustomerEmail = "sample@gmail.com",
            Timestamp = DateTime.Now
        };

        var result = MapOrderToOrderCreatedEvent.Map(order);
        
        result.CustomerEmail.ShouldBe(order.CustomerEmail);
        result.Amount.ShouldBe(order.Amount);
        result.OrderId.ShouldBe(order.OrderId);
    }
}