using Microsoft.Extensions.Logging;
using NSubstitute;
using OrderProcessor.NotificationService.Application.Messaging;
using OrderProcessor.NotificationService.Domain.Repositories;
using Shouldly;

namespace OrderProcessor.UnitTest.NotificationsService.Application.Messaging;

public class NotificationProcessorTest
{
    private readonly INotificationsRepository _notificationsRepository;
    private readonly NotificationProcessor _notificationProcessor;

    public NotificationProcessorTest()
    {
        _notificationsRepository = Substitute.For<INotificationsRepository>();
        _notificationProcessor = new NotificationProcessor(_notificationsRepository,
            Substitute.For<ILogger<NotificationService.Application.Services.NotificationsService>>());
    }

    [Fact]
    public async Task GivenPaymentDetails_WhenPaymentSucceededEventReceived_ProcessNotification()
    {
        var orderId = Guid.NewGuid();
        var paymentId = Guid.NewGuid();
        var amount = (decimal)89;
        var customerEmail = "sample@gmail.com";
        var timestamp = DateTime.Now;

        await _notificationProcessor.ProcessNotificationAsync(orderId, paymentId, amount, customerEmail, timestamp);
        
        _notificationsRepository.ReceivedCalls().Count().ShouldBe(1);
    }
}