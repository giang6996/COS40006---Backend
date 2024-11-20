using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatedb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuildingAddress",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "BuildingName",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "Documents");

            migrationBuilder.AddColumn<long>(
                name: "ApartmentId",
                table: "Documents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BuildingId",
                table: "Documents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ApartmentId",
                table: "Documents",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_BuildingId",
                table: "Documents",
                column: "BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Apartments_ApartmentId",
                table: "Documents",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Buildings_BuildingId",
                table: "Documents",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Apartments_ApartmentId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Buildings_BuildingId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_ApartmentId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_BuildingId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "Documents");

            migrationBuilder.AddColumn<string>(
                name: "BuildingAddress",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuildingName",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomNumber",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
