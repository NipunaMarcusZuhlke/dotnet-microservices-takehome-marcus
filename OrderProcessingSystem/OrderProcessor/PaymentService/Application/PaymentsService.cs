using OrderProcessor.PaymentService.Application.Dtos;

namespace OrderProcessor.PaymentService.Application;

public class PaymentsService(IPaymentRepository paymentRepository) : IPaymentsService
{
    public List<PaymentDto> GetAllProcessedPayments()
    {
        var payments = paymentRepository.GetAllProcessedPayments();
        return payments
            .Select(payment =>
                new PaymentDto(
                    payment.OrderId,
                    payment.PaymentId,
                    payment.Amount,
                    payment.Timestamp))
            .ToList();
    }
}