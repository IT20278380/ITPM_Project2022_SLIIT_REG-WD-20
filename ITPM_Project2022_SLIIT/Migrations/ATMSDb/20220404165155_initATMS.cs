using Microsoft.EntityFrameworkCore.Migrations;

namespace ITPM_Project2022_SLIIT.Migrations.ATMSDb
{
    public partial class initATMS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookTickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassengerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiredSeat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidRecipt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTickets", x => x.Id);
                });

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookTickets");

        }
    }
}
