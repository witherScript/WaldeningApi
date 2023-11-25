using Microsoft.EntityFrameworkCore;

namespace WaldeningApi.Models
{
  public class WaldeningApiContext : DbContext
  {
    public DbSet<Park> Parks { get; set; }

    public WaldeningApiContext(DbContextOptions<WaldeningApiContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {

      builder.Entity<Park>(entity =>
      {
        /**********************|********************
        # .HasConversion
        # stores the Activities array in the database as
        # a comma separated string, this is necessary because EFC expects Collection elements
        # to have primary keys, which is irrelevant in the context of an array of primitives.
        # In the API call, the second arrow function is called, splitting the Comma-separated 
        # string into a list.
        # see 
        ***********************|*******************/

        entity.Property(e => e.Activities)
            .HasConversion(
                v => string.Join(",", v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );
      });
      builder.Entity<Park>()
      .HasData(
        new Park
        {
          ParkId=1,
          State = "Texas",
          Name = "McKinley Falls State Park",
          Website = "https://tpwd.texas.gov/state-parks/mckinney-falls",
          IsNational = false,
          Activities = { "Biking", "Hiking", "Camping", "Swimming", "Rock Climbing" }
        },
        new Park
        {
          ParkId=2,
          State = "Arizona",
          Name = "Grand Canyon National Park",
          Website = "https://www.nationalparks.org/explore/parks/grand-canyon-national-park",
          IsNational = true,
          Activities = { "Hiking", "Canyoneering", "Camping", "Swimming", "Spelunking", "Rock Climbing" }
        },
        new Park
        {
          ParkId=3,
          State = "Texas",
          Name = "Pedernales Falls State Park",
          Website = "https://tpwd.texas.gov/state-parks/pedernales-falls",
          IsNational = false,
          Activities = { "Hiking", "Camping", "Swimming"}
        },
        new Park
        {
          ParkId=4,
          State = "New Mexico",
          Name = "White Sands National Park",
          Website = "https://www.nps.gov/whsa/index.htm",
          IsNational = true,
          Activities = { "Dune Sledding", "Hiking", "Cabin Rental", "Biking"}
        }
      );
    }
  }
}
