using Microsoft.EntityFrameworkCore;
using OrderProcessor.NotificationService.Application.Repositories;
using OrderProcessor.NotificationService.Domain;
using OrderProcessor.NotificationService.Infrastructure.Persistance;

namespace OrderProcessor.NotificationService.Infrastructure.Repositories;

public class InMemoryEfNotificationRepository(
    NotificationDbContext notificationDb,
    ILogger<InMemoryEfNotificationRepository> logger) : INotificationsRepository
{
    public async Task SaveNotificationAsync(Notification notification)
    {
        try
        {
            await notificationDb.Notifications.AddAsync(notification);
            await notificationDb.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to save notification for payment: {PaymentId} for order: {OrderId}",
                notification.PaymentId, notification.OrderId);
            throw;
        }
    }

    public async Task<List<Notification>> GetAllNotificationsAsync()
    {
        return await notificationDb.Notifications.ToListAsync();
    }
}