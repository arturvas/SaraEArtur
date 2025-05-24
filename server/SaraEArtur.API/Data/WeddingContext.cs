using Microsoft.EntityFrameworkCore;
using SaraEArtur.API.Models;

namespace SaraEArtur.API.Data;

public class WeddingContext : DbContext
{
    public DbSet<GiftsModel> Gifts { get; set; }
    
    public WeddingContext(DbContextOptions<WeddingContext> options) : base(options)
    {
        
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=myDataBase;User Id=myUsername;Password=myPassword;");
    //     base.OnConfiguring(optionsBuilder);
    // }
}
