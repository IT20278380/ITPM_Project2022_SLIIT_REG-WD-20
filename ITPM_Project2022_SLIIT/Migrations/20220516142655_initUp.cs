using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITPM_Project2022_SLIIT.Migrations
{
    public partial class initUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
