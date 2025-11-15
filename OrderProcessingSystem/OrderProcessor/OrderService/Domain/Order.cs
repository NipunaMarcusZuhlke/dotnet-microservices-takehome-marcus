namespace OrderProcessor.OrderService.Domain;

public class Order
{
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public string CustomerEmail { get; set; } = default!;
    public DateTime Timestamp { get; set; }
}