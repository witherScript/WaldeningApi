using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaldeningApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Parks",
                columns: table => new
                {
                    ParkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    State = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Website = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsNational = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Activities = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parks", x => x.ParkId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Parks",
                columns: new[] { "ParkId", "Activities", "IsNational", "Name", "State", "Website" },
                values: new object[,]
                {
                    { 1, "Biking,Hiking,Camping,Swimming,Rock Climbing", false, "McKinley Falls State Park", "Texas", "https://tpwd.texas.gov/state-parks/mckinney-falls" },
                    { 2, "Hiking,Canyoneering,Camping,Swimming,Spelunking,Rock Climbing", true, "Grand Canyon National Park", "Arizona", "https://www.nationalparks.org/explore/parks/grand-canyon-national-park" },
                    { 3, "Hiking,Camping,Swimming", false, "Pedernales Falls State Park", "Texas", "https://tpwd.texas.gov/state-parks/pedernales-falls" },
                    { 4, "Dune Sledding,Hiking,Cabin Rental,Biking", true, "White Sands National Park", "New Mexico", "https://www.nps.gov/whsa/index.htm" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parks");
        }
    }
}
