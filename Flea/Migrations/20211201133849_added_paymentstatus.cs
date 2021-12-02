using Microsoft.EntityFrameworkCore.Migrations;

namespace Flea.Migrations
{
    public partial class added_paymentstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Reservations",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Reservations");

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Reservations",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
