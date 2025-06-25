namespace Wedding.API.Core.Models;

public class Gift
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool Taken { get; set; } = false;
}
