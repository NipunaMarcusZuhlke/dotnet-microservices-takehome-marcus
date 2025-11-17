using NSubstitute;
using OrderProcessor.OrderService.Application.Dtos;
using OrderProcessor.OrderService.Application.Messaging;
using OrderProcessor.OrderService.Application.Repositories;
using OrderProcessor.OrderService.Application.Services;
using OrderProcessor.OrderService.Domain;
using Shouldly;

namespace OrderProcessor.UnitTest.OrderService.Application.Services;

public class OrdersServiceTest
{
    private readonly OrdersService _ordersService;
    private readonly IOrderRepository _mockOrderRepository;
    private readonly IOrderCreatedEventPublisher _mockOrderCreatedEventPublisher;

    public OrdersServiceTest()
    {
        _mockOrderRepository = Substitute.For<IOrderRepository>();
        _mockOrderCreatedEventPublisher = Substitute.For<IOrderCreatedEventPublisher>();
        _ordersService = new OrdersService(_mockOrderRepository, _mockOrderCreatedEventPublisher);
    }
    
    [Fact]
    public async Task GivenOrdersExist_WhenGetAllOrders_ReturnOrders()
    {
        var orderId = Guid.NewGuid();
        var order = new Order
        {
            OrderId = orderId,
            Amount = (decimal)89.09, 
            CustomerEmail = "sample@gmail.com",
            Timestamp = DateTime.Now
        };
        
        var listOfOrders = new List<Order> { order };
        _mockOrderRepository.GetAllOrdersAsync().Returns(listOfOrders);

        var result = await _ordersService.GetAllOrdersAsync();

        _mockOrderRepository.ReceivedCalls().Count().ShouldBe(1);
        result[0].OrderId.ShouldBe(order.OrderId);
        result[0].CustomerEmail.ShouldBe(order.CustomerEmail);
    }
    
    [Fact]
    public async Task GivenOrdersExist_WhenCreateOrder_ReturnCreatedOrder()
    {
        var createOrderDto = new CreateOrderRequestDto((decimal)89.09, "sample@gmail.com");

        var result = await _ordersService.CreateOrderAsync(createOrderDto);

        _mockOrderRepository.ReceivedCalls().Count().ShouldBe(1);
        _mockOrderCreatedEventPublisher.ReceivedCalls().Count().ShouldBe(1);
        result.CustomerEmail.ShouldBe(createOrderDto.CustomerEmail);
        result.Amount.ShouldBe(createOrderDto.Amount);
    }
    
    [Fact]
    public async Task GivenOrder_WhenGetOrderById_ReturnOrder()
    {
        var orderId = Guid.NewGuid();
        var order = new Order{
            OrderId = orderId,
            Amount = (decimal)89.09,
            CustomerEmail = "sample@gmail.com",
            Timestamp = DateTime.Now
            
        };
        _mockOrderRepository.GetOrderByIdAsync(orderId).Returns(order);

        var result = await _ordersService.GetOrderByIdAsync(orderId);

        result.ShouldNotBeNull();
        result.CustomerEmail.ShouldBe(order.CustomerEmail);
    }
}