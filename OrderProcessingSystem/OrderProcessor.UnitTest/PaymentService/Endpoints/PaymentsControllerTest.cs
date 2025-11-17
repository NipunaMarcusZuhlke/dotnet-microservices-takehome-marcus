using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using OrderProcessor.PaymentService.Application.Dtos;
using OrderProcessor.PaymentService.Application.Services;
using OrderProcessor.PaymentService.Endpoints;
using Shouldly;

namespace OrderProcessor.UnitTest.PaymentService.Endpoints;

public class PaymentsControllerTest
{
    private readonly PaymentsController _paymentsController;
    private readonly IPaymentsService _mockPaymentsService;

    public PaymentsControllerTest()
    {
        _mockPaymentsService = Substitute.For<IPaymentsService>();
        _paymentsController = new PaymentsController(_mockPaymentsService);
    }

    [Fact]
    public async Task GivenOrdersExist_WhenGetAllOrders_ReturnsOk_WithOrders()
    {
        var orderId = Guid.NewGuid();
        var paymentId = Guid.NewGuid();
        var paymentResponseDto = new PaymentResponseDto(orderId, paymentId, (decimal)89.09, DateTime.Now);
        var paymentResponseDtos = new List<PaymentResponseDto> { paymentResponseDto };

        _mockPaymentsService.GetAllProcessedPaymentsAsync().Returns(paymentResponseDtos);

        var result = await _paymentsController.GetAllProcessedPayments();

        result.ShouldBeOfType<OkObjectResult>();
        var okResult = (OkObjectResult)result;
        okResult.Value.ShouldBe(paymentResponseDtos);
    }
}