using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_Manager_Simulator_Backend.Migrations
{
    public partial class modifytransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BuyPrice",
                table: "Transactions",
                newName: "Price");

            migrationBuilder.AddColumn<int>(
                name: "TimeInTimestamp",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeInTimestamp",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Transactions",
                newName: "BuyPrice");
        }
    }
}
