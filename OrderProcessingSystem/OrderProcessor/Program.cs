using OrderProcessor.EventBus;
using OrderProcessor.Middleware;
using OrderProcessor.NotificationService.Infrastructure;
using OrderProcessor.OrderService.Infrastructure;
using OrderProcessor.PaymentService.Infrastructure;
using OrderProcessor.SharedDomain.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IEventBus, InMemoryChannelEventBus>();

builder.Services.RegisterOrderServiceDependencies();
builder.Services.RegisterPaymentServiceDependencies();
builder.Services.RegisterNotificationServiceDependencies();

builder.Services.AddOpenApiDocument(options =>
{
    options.PostProcess = document =>
    {
        document.OpenApi = "3.1.0";
        document.Info = new()
        {
            Version = "v1",
            Title = "Order processing system",
            Description = "The Order processing system that handles order creation, payment and notification",
        };
    };
});

var app = builder.Build();

var provider = app.Services;
var eventBus = provider.GetRequiredService<IEventBus>();

eventBus.Subscribe(provider.GetRequiredService<IEventHandler<OrderCreatedEvent>>());
eventBus.Subscribe(provider.GetRequiredService<IEventHandler<PaymentSucceededEvent>>());

app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();