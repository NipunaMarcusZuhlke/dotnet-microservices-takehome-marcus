using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using OrderProcessor.OrderService.Application.Dtos;
using OrderProcessor.OrderService.Application.Messaging;
using OrderProcessor.OrderService.Application.Services;
using OrderProcessor.OrderService.Application.Validators;
using OrderProcessor.OrderService.Domain.Repositories;
using OrderProcessor.OrderService.Infrastructure.Event;
using OrderProcessor.OrderService.Infrastructure.Persistence;
using OrderProcessor.OrderService.Infrastructure.Repositories;

namespace OrderProcessor.OrderService.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection RegisterOrderServiceDependencies(this IServiceCollection services)
    {
        services
            .AddDbContexts()
            .AddRepositories()
            .AddServices()
            .AddValidators()
            .AddMessagingHandlers();
        return services;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        services.AddDbContext<OrderDbContext>(options => options.UseInMemoryDatabase("OrdersDb"),
            ServiceLifetime.Singleton);
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IOrderRepository, InMemoryEfOrderRepository>();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IOrdersService, OrdersService>();
        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateOrderRequestDto>, CreateOrderRequestDtoValidator>();
        services.AddFluentValidationAutoValidation();
        return services;
    }

    private static IServiceCollection AddMessagingHandlers(this IServiceCollection services)
    {
        services.AddTransient<IOrderCreatedEventPublisher, OrderCreatedOrderCreatedEventPublisher>();
        return services;
    }
}