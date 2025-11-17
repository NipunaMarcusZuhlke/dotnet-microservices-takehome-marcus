using OrderProcessor.PaymentService.Application.Dtos;

namespace OrderProcessor.PaymentService.Application.Services;

public interface IPaymentsService
{
    Task<List<PaymentResponseDto>> GetAllProcessedPaymentsAsync();
}