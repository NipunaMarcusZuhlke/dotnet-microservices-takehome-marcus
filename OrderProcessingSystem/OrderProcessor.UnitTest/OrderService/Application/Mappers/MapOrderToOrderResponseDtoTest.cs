using OrderProcessor.OrderService.Application.Mappers;
using OrderProcessor.OrderService.Domain;
using Shouldly;

namespace OrderProcessor.UnitTest.OrderService.Application.Mappers;

public class MapOrderToOrderResponseDtoTest
{
    [Fact]
    public void GivenOrder_WhenMapOrderToOrderDto_ReturnOrderResponseDto()
    {
        var orderId = Guid.NewGuid();
        var order = new Order
        {
            OrderId = orderId,
            Amount = (decimal)89.09, 
            CustomerEmail = "sample@gmail.com",
            Timestamp = DateTime.Now
        };

        var result = MapOrderToOrderResponseDto.Map(order);
        
        result.CustomerEmail.ShouldBe(order.CustomerEmail);
        result.Amount.ShouldBe(order.Amount);
        result.OrderId.ShouldBe(order.OrderId);
        result.TimeStamp.ShouldBe(order.Timestamp);
    }
}