using Microsoft.EntityFrameworkCore.Migrations;

namespace Flea.Migrations
{
    public partial class various_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Events_EventId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReservationOwnerId",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Reservations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Customers",
                type: "VARCHAR",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Customers",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "Customers",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationOwnerId_EventId",
                table: "Reservations",
                columns: new[] { "ReservationOwnerId", "EventId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Events_EventId",
                table: "Reservations",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Events_EventId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReservationOwnerId_EventId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Reservations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Customers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Customers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationOwnerId",
                table: "Reservations",
                column: "ReservationOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Events_EventId",
                table: "Reservations",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
