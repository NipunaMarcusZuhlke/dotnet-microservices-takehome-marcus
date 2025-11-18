using Microsoft.AspNetCore.Mvc;
using OrderProcessor.Middleware;
using OrderProcessor.OrderService.Application.Dtos;
using OrderProcessor.OrderService.Application.Services;

namespace OrderProcessor.OrderService.Endpoints;

[ApiController]
[Route("api/orders")]
public class OrdersController(IOrdersService ordersService) : ControllerBase
{
    [HttpPost]
    [EndpointDescription("Create new order")]
    [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDto createOrderRequestDto)
    {
        var order = await ordersService.CreateOrderAsync(createOrderRequestDto);
        return CreatedAtAction(nameof(GetOrderById), new { order.OrderId }, order);
    }

    [HttpGet("{orderId:guid}")]
    [EndpointDescription("Get created order by given order id")]
    [ProducesResponseType(typeof(List<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<OrderResponseDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrderById([FromRoute] Guid orderId)
    {
        var order = await ordersService.GetOrderByIdAsync(orderId);
        return order is not null ? Ok(order) : NotFound();
    }

    [HttpGet]
    [EndpointDescription("Get all created orders")]
    [ProducesResponseType(typeof(List<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await ordersService.GetAllOrdersAsync();
        return Ok(orders);
    }
}