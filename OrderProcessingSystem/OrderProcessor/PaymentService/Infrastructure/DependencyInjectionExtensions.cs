using Microsoft.EntityFrameworkCore;
using OrderProcessor.EventBus;
using OrderProcessor.PaymentService.Application.Messaging;
using OrderProcessor.PaymentService.Application.Services;
using OrderProcessor.PaymentService.Domain.Repositories;
using OrderProcessor.PaymentService.Infrastructure.Event.Consumer;
using OrderProcessor.PaymentService.Infrastructure.Event.Publisher;
using OrderProcessor.PaymentService.Infrastructure.Persistence;
using OrderProcessor.PaymentService.Infrastructure.Repositories;
using OrderProcessor.SharedDomain.Events;

namespace OrderProcessor.PaymentService.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection RegisterPaymentServiceDependencies(this IServiceCollection services)
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
        services.AddDbContext<PaymentDbContext>(options => options.UseInMemoryDatabase("PaymentsDb"),
            ServiceLifetime.Singleton);
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IPaymentRepository, InMemoryEfPaymentRepository>();
        return services;
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IPaymentsService, PaymentsService>();
        return services;
    }
    
    private static IServiceCollection AddMessagingHandlers(this IServiceCollection services)
    {
        services.AddTransient<IPaymentProcessor, PaymentProcessor>();
        services.AddTransient<IPaymentSucceededEventPublisher, PaymentSucceededEventPublisher>();
        services.AddTransient<IEventHandler<OrderCreatedEvent>, OrderCreatedEventSubscription>();
        return services;
    }
}