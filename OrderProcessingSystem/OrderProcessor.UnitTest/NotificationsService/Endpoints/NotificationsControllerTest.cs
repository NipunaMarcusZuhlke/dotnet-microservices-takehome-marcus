using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using OrderProcessor.NotificationService.Application.Dtos;
using OrderProcessor.NotificationService.Application.Services;
using OrderProcessor.NotificationService.Endpoints;
using Shouldly;

namespace OrderProcessor.UnitTest.NotificationsService.Endpoints;

public class NotificationsControllerTest
{
    private readonly NotificationsController _notificationsController;
    private readonly INotificationsService _mockNotificationsService;

    public NotificationsControllerTest()
    {
        _mockNotificationsService = Substitute.For<INotificationsService>();
        _notificationsController = new NotificationsController(_mockNotificationsService);
    }
    
    [Fact]
    public async Task GivenNotificationsExist_WhenGetAllNotifications_ReturnsOk_WithNotifications()
    {
        var orderId = Guid.NewGuid();
        var notificationId = Guid.NewGuid();
        var paymentId = Guid.NewGuid();
        var notificationResponseDto = new NotificationResponseDto(notificationId, orderId, paymentId,(decimal)89.09, "sample@gmail.com", DateTime.Now);
        var notificationResponseDtos = new List<NotificationResponseDto> { notificationResponseDto };
        _mockNotificationsService.GetAllNotificationsAsync().Returns(notificationResponseDtos);

        var result = await _notificationsController.GetAllNotifications();

        result.ShouldBeOfType<OkObjectResult>();
        var okResult = (OkObjectResult)result;
        okResult.Value.ShouldBe(notificationResponseDtos);
    }
}