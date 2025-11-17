using Microsoft.AspNetCore.Mvc;
using OrderProcessor.Middleware;
using OrderProcessor.PaymentService.Application.Dtos;
using OrderProcessor.PaymentService.Application.Services;

namespace OrderProcessor.PaymentService.Endpoints;

[ApiController]
[Route("api/payments")]
public class PaymentsController(IPaymentsService paymentsService): ControllerBase
{
    [HttpGet]
    [EndpointDescription("Get all processed payments")]
    [ProducesResponseType(typeof(PaymentResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllProcessedPayments()
    {
        var payments = await paymentsService.GetAllProcessedPaymentsAsync();
        return Ok(payments);
    }
}