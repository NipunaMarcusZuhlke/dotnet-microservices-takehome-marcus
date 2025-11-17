using NSubstitute;
using OrderProcessor.PaymentService.Application.Repositories;
using OrderProcessor.PaymentService.Application.Services;
using OrderProcessor.PaymentService.Domain;
using Shouldly;

namespace OrderProcessor.UnitTest.PaymentService.Application.Services;

public class PaymentsServiceTest
{
    private readonly PaymentsService _paymentsService;
    private readonly IPaymentRepository _mockPaymentRepository;

    public PaymentsServiceTest()
    {
        _mockPaymentRepository = Substitute.For<IPaymentRepository>();
        _paymentsService = new PaymentsService(_mockPaymentRepository);
    }

    [Fact]
    public async Task GivenPaymentsExist_WhenGetAllProcessedPayments_ReturnPayments()
    {
        var orderId = Guid.NewGuid();
        var paymentId = Guid.NewGuid();
        var payment = new Payment
        {
            OrderId = orderId,
            PaymentId = paymentId,
            Amount = (decimal)89.09,
            CustomerEmail = "sample@gmail.com",
            Timestamp = DateTime.Now
        };

        var listOfPayments = new List<Payment> { payment };
        _mockPaymentRepository.GetAllProcessedPaymentsAsync().Returns(listOfPayments);

        var result = await _paymentsService.GetAllProcessedPaymentsAsync();

        _mockPaymentRepository.ReceivedCalls().Count().ShouldBe(1);
        result[0].OrderId.ShouldBe(orderId);
        result[0].PaymentId.ShouldBe(paymentId);
    }
}