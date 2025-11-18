using OrderProcessor.NotificationService.Application.Mappers;
using OrderProcessor.NotificationService.Domain;
using Shouldly;

namespace OrderProcessor.UnitTest.NotificationsService.Application.Mappers;

public class MapNotificationToNotificationResponseDtoTest
{
    [Fact]
    public void GivenNotification_WhenMapNotificationToNotificationResponseDto_ReturnNotificationResponseDto()
    {
        var orderId = Guid.NewGuid();
        var paymentId = Guid.NewGuid();
        var notificationId = Guid.NewGuid();
        var notification = new Notification
        {
            NotificationId = notificationId,
            OrderId = orderId,
            PaymentId = paymentId,
            Amount = (decimal)89.09, 
            CustomerEmail = "sample@gmail.com",
            PaymentTimestamp = DateTime.Now,
            NotificationTimestamp = DateTime.Now,
        };

        var result = MapNotificationToNotificationResponseDto.MapToNotificationResponseDto(notification);
        
        result.PaymentId.ShouldBe(paymentId);
        result.NotificationId.ShouldBe(notificationId);
        result.OrderId.ShouldBe(orderId);
    }
}