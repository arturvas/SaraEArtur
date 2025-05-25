namespace SaraEArtur.API.Models;

public class GiftModel
{
    public GiftModel(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
    }
    
    public Guid Id { get; init; }
    public string Name { get; private set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsAvailable { get; set; } = true;
    public string? ReservedBy { get; set; }
    public DateTime? ReservedAt { get; set; }
    
    public void ChangeName(string newName)
    {
        Name = newName;
    }

    public void SetInactive()
    {
        Name = "Inactive";
    }
}
