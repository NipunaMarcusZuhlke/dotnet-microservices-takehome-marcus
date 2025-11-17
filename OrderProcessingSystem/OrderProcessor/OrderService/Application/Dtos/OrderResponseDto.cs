namespace OrderProcessor.OrderService.Application.Dtos;

public record OrderResponseDto(Guid OrderId, decimal Amount, string CustomerEmail, DateTime TimeStamp);