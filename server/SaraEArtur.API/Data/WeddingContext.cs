using Microsoft.EntityFrameworkCore;
using SaraEArtur.API.Models;

namespace SaraEArtur.API.Data;

public class WeddingContext : DbContext
{
    public DbSet<GiftModel> Gifts { get; set; }
    
    public WeddingContext(DbContextOptions<WeddingContext> options) : base(options)
    {
        
    }
}
