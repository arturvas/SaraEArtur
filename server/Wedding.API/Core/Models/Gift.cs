namespace Wedding.API.Core.Models;

public class Gift
{
    public int Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int TimesTaken { get; set; } = 0;
    public DateTime? LastTakenAt { get; set; }
    public string? LastPayerFullName { get; set; }
}
