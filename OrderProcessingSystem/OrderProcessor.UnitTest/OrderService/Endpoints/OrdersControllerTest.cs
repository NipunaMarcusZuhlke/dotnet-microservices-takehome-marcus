using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using OrderProcessor.OrderService.Application;
using OrderProcessor.OrderService.Application.Dtos;
using OrderProcessor.OrderService.Endpoints;
using Shouldly;

namespace OrderProcessor.UnitTest.OrderService.Endpoints;

public class OrdersControllerTest
{
    private readonly OrdersController _ordersController;
    private readonly IOrdersService _mockOrdersService;

    public OrdersControllerTest()
    {
        _mockOrdersService = Substitute.For<IOrdersService>();
        _ordersController = new OrdersController(_mockOrdersService);
    }
    
    [Fact]
    public void GivenOrdersExist_WhenGetAllOrders_ReturnsOk_WithOrders()
    {
        var orderId = Guid.NewGuid();
        var order = new OrderDto(orderId, (decimal)89.09, "sample@gmail.com", DateTime.Now);
        var listOfOrders = new List<OrderDto> { order };
        _mockOrdersService.GetAllOrders().Returns(listOfOrders);

        var result = _ordersController.GetAllOrders();

        result.ShouldBeOfType<OkObjectResult>();
        var okResult = (OkObjectResult)result;
        okResult.Value.ShouldBe(listOfOrders);
    }
    
    [Fact]
    public void GivenOrder_WhenCreateOrder_ReturnsCreated()
    {
        var order = new CreateOrderDto((decimal)89.09, "sample@gmail.com");

        var result = _ordersController.CreateOrder(order);

        result.ShouldBeOfType<CreatedAtActionResult>();
        var createdAtActionResult = (CreatedAtActionResult)result;
        createdAtActionResult.StatusCode.ShouldBe(StatusCodes.Status201Created);
    }
}