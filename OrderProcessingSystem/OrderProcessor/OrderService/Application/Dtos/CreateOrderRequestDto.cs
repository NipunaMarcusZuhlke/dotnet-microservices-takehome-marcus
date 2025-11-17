namespace OrderProcessor.OrderService.Application.Dtos;

public record CreateOrderRequestDto(decimal Amount, string CustomerEmail);