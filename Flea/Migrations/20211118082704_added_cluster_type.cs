using Microsoft.EntityFrameworkCore.Migrations;

namespace Flea.Migrations
{
    public partial class added_cluster_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Events_PreviousEventId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_PreviousEventId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "PreviousEventId",
                table: "Events");

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

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Cluster",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Cluster");

            migrationBuilder.AddColumn<int>(
                name: "PreviousEventId",
                table: "Events",
                type: "integer",
                nullable: true);

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
                name: "IX_Events_PreviousEventId",
                table: "Events",
                column: "PreviousEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Events_PreviousEventId",
                table: "Events",
                column: "PreviousEventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
