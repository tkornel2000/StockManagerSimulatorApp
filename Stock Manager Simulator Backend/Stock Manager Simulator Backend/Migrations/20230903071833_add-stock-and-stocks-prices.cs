using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_Manager_Simulator_Backend.Migrations
{
    public partial class addstockandstocksprices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dayOpen",
                table: "StocksPrices",
                newName: "DayOpen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DayOpen",
                table: "StocksPrices",
                newName: "dayOpen");
        }
    }
}
