using Microsoft.EntityFrameworkCore;
using OrderProcessor.NotificationService.Domain;

namespace OrderProcessor.NotificationService.Infrastructure.Persistance;

public class NotificationDbContext(DbContextOptions<NotificationDbContext> options) : DbContext(options)
{
    public DbSet<Notification> Notifications { get; init; } = default!;
}