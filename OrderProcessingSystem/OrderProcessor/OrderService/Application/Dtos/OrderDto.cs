namespace OrderProcessor.OrderService.Application.Dtos;

public record OrderDto(Guid OrderId, decimal Amount, string CustomerEmail, DateTime TimeStamp);