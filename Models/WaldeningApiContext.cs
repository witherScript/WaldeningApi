using Microsoft.EntityFrameworkCore;

namespace WaldeningApi.Models
{
  public class WaldeningApiContext : DbContext
  {
    public DbSet<Park> Parks { get; set; }

    public WaldeningApiContext(DbContextOptions<WaldeningApiContext> options) : base(options)
    {
    }
        // protected override void OnModelCreating(ModelBuilder builder)
        // {
        //   builder.Entity<Park>()
        //   .HasData(

        //   );
        // }
    }
}