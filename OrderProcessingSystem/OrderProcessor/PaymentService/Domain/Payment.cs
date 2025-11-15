namespace OrderProcessor.PaymentService.Domain;

public class Payment
{
    public Guid OrderId { get; set; }
    public Guid PaymentId { get; set; }
    public decimal Amount { get; set; }
    public string CustomerEmail { get; set; } = default!;
    public DateTime Timestamp { get; set; }
}
