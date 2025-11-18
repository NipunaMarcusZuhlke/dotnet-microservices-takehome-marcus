using OrderProcessor.PaymentService.Application.Dtos;
using OrderProcessor.PaymentService.Application.Mappers;
using OrderProcessor.PaymentService.Domain.Repositories;

namespace OrderProcessor.PaymentService.Application.Services;

public class PaymentsService(IPaymentRepository paymentRepository) : IPaymentsService
{
    public async Task<List<PaymentResponseDto>> GetAllProcessedPaymentsAsync()
    {
        var payments = await paymentRepository.GetAllProcessedPaymentsAsync();
        return payments
            .Select(MapPaymentToPaymentResponseDto.Map)
            .ToList();
    }
}