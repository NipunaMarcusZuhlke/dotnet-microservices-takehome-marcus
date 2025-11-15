namespace OrderProcessor.NotificationService.Domain;

public class Notification
{
    public Guid NotificationId { get; set; }
    public Guid OrderId { get; set; }
    public Guid PaymentId { get; set; }
    public decimal Amount { get; set; }
    public string CustomerEmail { get; set; } = default!;
    public DateTime PaymentTimestamp { get; set; }
    public DateTime NotificationTimestamp { get; set; }
}