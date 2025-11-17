using OrderProcessor.PaymentService.Application.Mappers;
using OrderProcessor.PaymentService.Domain;
using Shouldly;

namespace OrderProcessor.UnitTest.PaymentService.Application.Mappers;

public class MapPaymentToPaymentResponseDtoTest
{
    [Fact]
    public void GivenPayment_WhenMapPaymentToPaymentResponseDto_ReturnPaymentResponseDto()
    {
        var orderId = Guid.NewGuid();
        var paymentId = Guid.NewGuid();
        var order = new Payment
        {
            OrderId = orderId,
            PaymentId = paymentId,
            Amount = (decimal)89.09, 
            CustomerEmail = "sample@gmail.com",
            Timestamp = DateTime.Now
        };

        var result = MapPaymentToPaymentResponseDto.Map(order);
        
        result.PaymentId.ShouldBe(paymentId);
        result.Amount.ShouldBe(order.Amount);
        result.OrderId.ShouldBe(orderId);
    }
}