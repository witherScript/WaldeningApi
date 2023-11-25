
namespace WaldeningApi.Models
{
  using Microsoft.EntityFrameworkCore;
  using System;
  using System.Collections.Generic;
  using System.Linq;


  public class WaldeningApiContext : DbContext
  {
      public DbSet<Park> Parks { get; set; }

      public WaldeningApiContext(DbContextOptions<WaldeningApiContext> options) : base(options)
      {
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
          // the Activities array is persisted as a comma-separated string of activities,
          // when it is pulled out it can be split into an array
          modelBuilder.Entity<Park>(entity =>
          {
              entity.Property(e => e.Activities)
                  .HasConversion(
                      v => string.Join(",", v),
                      v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                  );
          });

          SeedData(modelBuilder);
      }

      private static void SeedData(ModelBuilder modelBuilder)
      {
          // EFC matches schema of Park objects, type casts anonymous objects into instances of
          // Park
          var parks = new List<Park>
          {
              new() {
                  ParkId = 1,
                  State = "Texas",
                  Name = "McKinley Falls State Park",
                  Website = "https://tpwd.texas.gov/state-parks/mckinney-falls",
                  IsNational = false,
                  Activities = new List<string> { "Biking", "Hiking", "Camping", "Swimming", "Rock Climbing" }
              },
              new() {
                  ParkId = 2,
                  State = "Arizona",
                  Name = "Grand Canyon National Park",
                  Website = "https://www.nationalparks.org/explore/parks/grand-canyon-national-park",
                  IsNational = true,
                  Activities = new List<string> { "Hiking", "Canyoneering", "Camping", "Swimming", "Spelunking", "Rock Climbing" }
              },
              new() {
                  ParkId = 3,
                  State = "Texas",
                  Name = "Pedernales Falls State Park",
                  Website = "https://tpwd.texas.gov/state-parks/pedernales-falls",
                  IsNational = false,
                  Activities = new List<string> { "Hiking", "Camping", "Swimming" }
              },
              new() {
                  ParkId = 4,
                  State = "New Mexico",
                  Name = "White Sands National Park",
                  Website = "https://www.nps.gov/whsa/index.htm",
                  IsNational = true,
                  Activities = new List<string> { "Dune Sledding", "Hiking", "Cabin Rental", "Biking" }
              }
          };

          modelBuilder.Entity<Park>().HasData(parks);
      }
  }
}
