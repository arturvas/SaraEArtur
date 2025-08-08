namespace Wedding.API.Core.Models;

public class GiftOrder
{
    public int Id { get; set; }
    public int? GiftId { get; set; }
    public string? Title { get; set; }
    public string PayerFullName { get; set; } = string.Empty;
    public string? PayerFirstName { get; set; }
    public string? PayerLastName { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidAt { get; set; }
    public string? PayerEmail { get; set; }
    public string? PaymentType { get; set; }
}
