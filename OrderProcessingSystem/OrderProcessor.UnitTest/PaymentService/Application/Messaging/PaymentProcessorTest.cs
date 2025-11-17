using Microsoft.Extensions.Logging;
using NSubstitute;
using OrderProcessor.PaymentService.Application.Messaging;
using OrderProcessor.PaymentService.Application.Repositories;
using Shouldly;

namespace OrderProcessor.UnitTest.PaymentService.Application.Messaging;

public class PaymentProcessorTest
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentSucceededEventPublisher _paymentSucceededEventPublisher;
    private readonly PaymentProcessor _paymentProcessor;

    public PaymentProcessorTest()
    {
        _paymentRepository = Substitute.For<IPaymentRepository>();
        _paymentSucceededEventPublisher = Substitute.For<IPaymentSucceededEventPublisher>();
        _paymentProcessor = new PaymentProcessor(_paymentRepository, _paymentSucceededEventPublisher,
            Substitute.For<ILogger<PaymentProcessor>>());
    }

    [Fact]
    public async Task GivenOrderDetails_WhenOrderCreatedEventReceive_ProcessPayment()
    {
        var orderId = Guid.NewGuid();
        var amount = (decimal)49.09;
        var customerEmail = "sample@gmail.com";

        await _paymentProcessor.ProcessPaymentAsync(orderId, amount, customerEmail);
        
        _paymentRepository.ReceivedCalls().Count().ShouldBe(1);
        _paymentSucceededEventPublisher.ReceivedCalls().Count().ShouldBe(1);
    }
}