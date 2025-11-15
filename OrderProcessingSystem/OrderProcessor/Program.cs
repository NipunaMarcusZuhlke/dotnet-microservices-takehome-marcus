using EventBus;
using OrderProcessor.NotificationService.Application;
using OrderProcessor.NotificationService.Infrastructure.Event;
using OrderProcessor.NotificationService.Infrastructure.Persistance;
using OrderProcessor.NotificationService.Infrastructure.Repositories;
using OrderProcessor.OrderService.Application;
using OrderProcessor.OrderService.Infrastructure.Event;
using OrderProcessor.OrderService.Infrastructure.Persistance;
using OrderProcessor.OrderService.Infrastructure.Repositories;
using OrderProcessor.PaymentService.Application;
using OrderProcessor.PaymentService.Infrastructure.Event.Consumer;
using OrderProcessor.PaymentService.Infrastructure.Event.Publisher;
using OrderProcessor.PaymentService.Infrastructure.Persistance;
using OrderProcessor.PaymentService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IEventBus, InMemoryEventBus>();

builder.Services.AddSingleton<IPaymentDataStore, InMemoryPaymentDataStore>();
builder.Services.AddSingleton<IOrderDataStore, InMemoryOrderDataStore>();
builder.Services.AddSingleton<INotificationDataStore, InMemoryNotificationDataStore>();

builder.Services.AddTransient<IOrderRepository, InMemoryOrderRepository>();
builder.Services.AddTransient<IOrdersService, OrdersService>();
builder.Services.AddTransient<IOrderCreatedEventPublisher, OrderCreatedOrderCreatedEventPublisher>();

builder.Services.AddTransient<IPaymentsService, PaymentsService>();
builder.Services.AddTransient<IPaymentRepository, InMemoryPaymentRepository>();
builder.Services.AddTransient<IPaymentProcessor, PaymentProcessor>();
builder.Services.AddTransient<IPaymentSucceededEventPublisher, PaymentSucceededEventPublisher>();

builder.Services.AddTransient<INotificationsService, NotificationsService>();
builder.Services.AddTransient<INotificationsRepository, InMemoryNotificationRepository>();
builder.Services.AddTransient<INotificationProcessor, NotificationProcessor>();

var app = builder.Build();

var provider = app.Services;
var eventBus = provider.GetRequiredService<IEventBus>();
PaymentEventSubscription.Register(eventBus, provider);
NotificationEventSubscription.Register(eventBus, provider);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
