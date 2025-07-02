using Microsoft.EntityFrameworkCore;
using Wedding.API.Core.Models;

namespace Wedding.API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Gift> Gifts => Set<Gift>();
}
