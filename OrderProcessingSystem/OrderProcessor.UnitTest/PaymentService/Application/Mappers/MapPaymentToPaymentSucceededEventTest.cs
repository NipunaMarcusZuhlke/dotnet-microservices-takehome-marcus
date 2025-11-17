using OrderProcessor.PaymentService.Application.Mappers;
using OrderProcessor.PaymentService.Domain;
using Shouldly;

namespace OrderProcessor.UnitTest.PaymentService.Application.Mappers;

public class MapPaymentToPaymentSucceededEventTest
{
    [Fact]
    public void GivenPayment_WhenMapToPaymentSucceededEvent_ReturnPaymentSucceededEvent()
    {
        var orderId = Guid.NewGuid();
        var paymentId = Guid.NewGuid();
        var payment = new Payment
        {
            OrderId = orderId,
            PaymentId = paymentId,
            Amount = (decimal)89.09,
            CustomerEmail = "sample@gmail.com",
            Timestamp = DateTime.Now
        };

        var result = MapPaymentToPaymentSucceededEvent.Map(payment);

        result.CustomerEmail.ShouldBe(payment.CustomerEmail);
        result.Amount.ShouldBe(payment.Amount);
        result.OrderId.ShouldBe(orderId);
        result.PaymentId.ShouldBe(paymentId);
    }
}