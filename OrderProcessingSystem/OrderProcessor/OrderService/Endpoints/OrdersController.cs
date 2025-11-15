using Microsoft.AspNetCore.Mvc;
using OrderProcessor.OrderService.Application;
using OrderProcessor.OrderService.Application.Dtos;

namespace OrderProcessor.OrderService.Endpoints;

[ApiController]
[Route("api/orders")]
public class OrdersController(IOrdersService ordersService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateOrder([FromBody] CreateOrderDto createOrderDto)
    {
        ordersService.CreateOrder(createOrderDto);
        return CreatedAtAction(nameof(GetAllOrders), null);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetAllOrders()
    {
        var orders = ordersService.GetAllOrders();
        return Ok(orders);
    }
}