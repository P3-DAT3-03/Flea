using Microsoft.EntityFrameworkCore.Migrations;

namespace Flea.Migrations
{
    public partial class reservation_arrived : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Arrived",
                table: "Reservations",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Arrived",
                table: "Reservations");
        }
    }
}
