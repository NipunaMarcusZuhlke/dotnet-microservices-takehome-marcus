using NSubstitute;
using OrderProcessor.NotificationService.Application.Repositories;
using OrderProcessor.NotificationService.Domain;
using Shouldly;

namespace OrderProcessor.UnitTest.NotificationsService.Application.Services;

public class NotificationsServiceTest
{
    private readonly NotificationService.Application.Services.NotificationsService _notificationsService;
    private readonly INotificationsRepository _mockNotificationRepository;

    public NotificationsServiceTest()
    {
        _mockNotificationRepository = Substitute.For<INotificationsRepository>();
        _notificationsService = new NotificationService.Application.Services.NotificationsService(_mockNotificationRepository);
    }

    [Fact]
    public async Task GivenNotificationsExist_WhenGetAllNotifications_ReturnNotifications()
    {
        var notification = new Notification
        {
            OrderId = Guid.NewGuid(),
            PaymentId = Guid.NewGuid(),
            NotificationId = Guid.NewGuid(),
            Amount = (decimal)89.09,
            CustomerEmail = "sample@gmail.com",
            PaymentTimestamp = DateTime.Now,
            NotificationTimestamp = DateTime.Now
        };

        var listOfNotifications = new List<Notification> { notification };
        _mockNotificationRepository.GetAllNotificationsAsync().Returns(listOfNotifications);

        var result = await _notificationsService.GetAllNotificationsAsync();

        _mockNotificationRepository.ReceivedCalls().Count().ShouldBe(1);
        result[0].OrderId.ShouldBe(notification.OrderId);
        result[0].PaymentId.ShouldBe(notification.PaymentId);
        result[0].NotificationId.ShouldBe(notification.NotificationId);
    }
}