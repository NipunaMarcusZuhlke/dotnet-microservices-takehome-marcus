namespace OrderProcessor.OrderService.Application.Dtos;

public record CreateOrderDto(decimal Amount, string CustomerEmail);