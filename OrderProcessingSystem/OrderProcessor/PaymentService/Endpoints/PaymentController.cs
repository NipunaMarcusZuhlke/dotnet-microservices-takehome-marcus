using Microsoft.AspNetCore.Mvc;
using OrderProcessor.PaymentService.Application;
using OrderProcessor.PaymentService.Application.Dtos;

namespace OrderProcessor.PaymentService.Endpoints;

[ApiController]
[Route("api/payments")]
public class PaymentController(IPaymentsService paymentsService): ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status200OK)]
    public IActionResult GetAllProcessedPayments()
    {
        var payments = paymentsService.GetAllProcessedPayments();
        return Ok(payments);
    }
}