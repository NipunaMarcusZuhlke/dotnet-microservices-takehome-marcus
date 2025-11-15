namespace OrderProcessor.SharedDomain.Events;

public record PaymentSucceededEvent(Guid OrderId, Guid PaymentId, decimal Amount, string CustomerEmail, DateTime TimeStamp);