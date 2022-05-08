using Microsoft.EntityFrameworkCore.Migrations;

namespace ITPM_Project2022_SLIIT.Migrations.ATMSDb
{
<<<<<<<< HEAD:ITPM_Project2022_SLIIT/Migrations/ATMSDb/20220507154932_init2.cs
    public partial class init2 : Migration
========
    public partial class initATMS3 : Migration
>>>>>>>> 179b72074e5f8a69bb524bcb71b10ae3b9b44678:ITPM_Project2022_SLIIT/Migrations/ATMSDb/20220405141838_initATMS3.cs
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
<<<<<<<< HEAD:ITPM_Project2022_SLIIT/Migrations/ATMSDb/20220507154932_init2.cs
                name: "Notification",
========
                name: "OrderList",
>>>>>>>> 179b72074e5f8a69bb524bcb71b10ae3b9b44678:ITPM_Project2022_SLIIT/Migrations/ATMSDb/20220405141838_initATMS3.cs
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightName = table.Column<string>(type: "nvarchar(max)", nullable: true),
<<<<<<<< HEAD:ITPM_Project2022_SLIIT/Migrations/ATMSDb/20220507154932_init2.cs
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
========
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderList", x => x.Id);
>>>>>>>> 179b72074e5f8a69bb524bcb71b10ae3b9b44678:ITPM_Project2022_SLIIT/Migrations/ATMSDb/20220405141838_initATMS3.cs
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
<<<<<<<< HEAD:ITPM_Project2022_SLIIT/Migrations/ATMSDb/20220507154932_init2.cs
                name: "Notification");
========
                name: "OrderList");
>>>>>>>> 179b72074e5f8a69bb524bcb71b10ae3b9b44678:ITPM_Project2022_SLIIT/Migrations/ATMSDb/20220405141838_initATMS3.cs
        }
    }
}
