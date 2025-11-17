using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using OrderProcessor.Middleware;
using OrderProcessor.OrderService.Application.Dtos;
using OrderProcessor.OrderService.Application.Services;
using OrderProcessor.OrderService.Application.Validators;
using OrderProcessor.OrderService.Endpoints;
using Shouldly;

namespace OrderProcessor.UnitTest.OrderService.Endpoints;

public class OrdersControllerTest
{
    private readonly OrdersController _ordersController;
    private readonly IOrdersService _mockOrdersService;

    public OrdersControllerTest()
    {
        var createOrderDtoValidator = new CreateOrderRequestDtoValidator();
        _mockOrdersService = Substitute.For<IOrdersService>();
        _ordersController = new OrdersController(_mockOrdersService, createOrderDtoValidator);
    }
    
    [Fact]
    public async Task GivenOrdersExist_WhenGetAllOrders_ReturnsOk_WithOrders()
    {
        var orderId = Guid.NewGuid();
        var order = new OrderResponseDto(orderId, (decimal)89.09, "sample@gmail.com", DateTime.Now);
        var listOfOrders = new List<OrderResponseDto> { order };
        _mockOrdersService.GetAllOrdersAsync().Returns(listOfOrders);

        var result = await _ordersController.GetAllOrders();

        result.ShouldBeOfType<OkObjectResult>();
        var okResult = (OkObjectResult)result;
        okResult.Value.ShouldBe(listOfOrders);
    }
    
    [Fact]
    public async Task GivenOrder_WhenCreateOrder_ReturnsCreatedWithActionResult_WithCreatedOrder()
    {
        var order = new CreateOrderRequestDto((decimal)89.09, "sample@gmail.com");
        var createdOrderDto = new OrderResponseDto(Guid.NewGuid(), (decimal)89.09, "sample@gmail.com", DateTime.Now);
        _mockOrdersService.CreateOrderAsync(order).Returns(createdOrderDto);

        var result = await _ordersController.CreateOrder(order);

        result.ShouldBeOfType<CreatedAtActionResult>();
        var createdAtActionResult = (CreatedAtActionResult)result;
        createdAtActionResult.StatusCode.ShouldBe(StatusCodes.Status201Created);
        createdAtActionResult.Value.ShouldBe(createdOrderDto);
    }
    
    [Fact]
    public async Task GivenInValidValuesForOrder_WhenCreateOrder_ReturnsBadRequest_WithErrors()
    {
        var order = new CreateOrderRequestDto((decimal)89.09, "sample");
        var createdOrderDto = new OrderResponseDto(Guid.NewGuid(), (decimal)89.09, "sample", DateTime.Now);
        _mockOrdersService.CreateOrderAsync(order).Returns(createdOrderDto);

        var result = await _ordersController.CreateOrder(order);

        result.ShouldBeOfType<BadRequestObjectResult>();
        var badRequestResult = (BadRequestObjectResult)result;
        badRequestResult.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        badRequestResult.Value.ShouldBeOfType(typeof(ApiError));
        var apiError = (ApiError)badRequestResult.Value;
        apiError.Message.ShouldBe("Order request include invalid values");
        apiError.ErrorDetails[0].ShouldBe("Invalid Customer Email");
    }
    
    [Fact]
    public async Task GivenOrder_WhenGetOrderById_ReturnOk_WithOrder()
    {
        var orderId = Guid.NewGuid();
        var orderDto = new OrderResponseDto(orderId, (decimal)89.09, "sample@gmail.com", DateTime.Now);
        _mockOrdersService.GetOrderByIdAsync(orderId).Returns(orderDto);

        var result = await _ordersController.GetOrderById(orderId);

        result.ShouldBeOfType<OkObjectResult>();
        var okResult = (OkObjectResult)result;
        okResult.Value.ShouldBe(orderDto);
    }
}