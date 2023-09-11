using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_Manager_Simulator_Backend.Migrations
{
    public partial class adddatetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Datetime",
                table: "Ranks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Datetime",
                table: "Ranks");
        }
    }
}
