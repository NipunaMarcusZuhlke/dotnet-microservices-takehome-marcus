using OrderProcessor.PaymentService.Application.Dtos;

namespace OrderProcessor.PaymentService.Application;

public interface IPaymentsService
{
    List<PaymentDto> GetAllProcessedPayments();
}