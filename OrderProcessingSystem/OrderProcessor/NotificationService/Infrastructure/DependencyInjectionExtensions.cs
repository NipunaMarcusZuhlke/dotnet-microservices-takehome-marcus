using EventBus;
using Microsoft.EntityFrameworkCore;
using OrderProcessor.NotificationService.Application.Messaging;
using OrderProcessor.NotificationService.Application.Repositories;
using OrderProcessor.NotificationService.Application.Services;
using OrderProcessor.NotificationService.Infrastructure.Event;
using OrderProcessor.NotificationService.Infrastructure.Persistance;
using OrderProcessor.NotificationService.Infrastructure.Repositories;
using OrderProcessor.SharedDomain.Events;

namespace OrderProcessor.NotificationService.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection RegisterNotificationServiceDependencies(this IServiceCollection services)
    {
        services
            .AddDbContexts()
            .AddRepositories()
            .AddServices()
            .AddMessagingHandlers();
        return services;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        services.AddDbContext<NotificationDbContext>(options => options.UseInMemoryDatabase("NotificationsDb"),
            ServiceLifetime.Singleton);
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<INotificationsRepository, InMemoryEfNotificationRepository>();
        return services;
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<INotificationsService, NotificationsService>();
        return services;
    }
    
    private static IServiceCollection AddMessagingHandlers(this IServiceCollection services)
    {
        services.AddTransient<INotificationProcessor, NotificationProcessor>();
        services.AddTransient<IEventHandler<PaymentSucceededEvent>, PaymentSucceededEventSubscription>();
        return services;
    }
}