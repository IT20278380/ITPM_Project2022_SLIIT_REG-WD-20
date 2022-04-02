using Microsoft.EntityFrameworkCore.Migrations;

namespace ITPM_Project2022_SLIIT.Migrations.ATMSDb
{
    public partial class initATMS1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlightList",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstClassPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BsClassPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriEconomyClassPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EconomyClassPrice = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightList", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightList");
        }
    }
}
