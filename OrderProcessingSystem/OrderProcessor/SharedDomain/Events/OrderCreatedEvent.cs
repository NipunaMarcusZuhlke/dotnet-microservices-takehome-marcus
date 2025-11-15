namespace OrderProcessor.SharedDomain.Events;

public record OrderCreatedEvent(Guid OrderId, decimal Amount, string CustomerEmail);