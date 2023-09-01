using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_Manager_Simulator_Backend.Migrations
{
    public partial class fixbirthOfDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirtOfDate",
                table: "Users",
                newName: "BirthOfDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthOfDate",
                table: "Users",
                newName: "BirtOfDate");
        }
    }
}
