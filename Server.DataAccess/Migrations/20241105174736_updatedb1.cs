using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatedb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UrbanAreas",
                columns: new[] { "Id", "UrbanAddress" },
                values: new object[,]
                {
                    { 1L, "Urban Area 1" },
                    { 2L, "Urban Area 2" },
                    { 3L, "Urban Area 3" }
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "BuildingAddress", "BuildingName", "ModuleId", "NumberFloor", "TenantId", "UrbanId" },
                values: new object[,]
                {
                    { 1L, "123 Main St", "Sunrise Tower", 1L, 10, 1L, 1L },
                    { 2L, "456 Elm St", "Moonlight Apartments", 1L, 15, 2L, 2L },
                    { 3L, "789 Elm St", "Building C", 1L, 8, 3L, 3L }
                });

            migrationBuilder.InsertData(
                table: "Apartments",
                columns: new[] { "Id", "Available", "BuildingId", "RoomNumber" },
                values: new object[,]
                {
                    { 1L, "Yes", 1L, 101 },
                    { 2L, "No", 1L, 102 },
                    { 3L, "Yes", 2L, 201 }
                });

            migrationBuilder.InsertData(
                table: "ApartmentDetails",
                columns: new[] { "Id", "ApartmentId", "NumBathroom", "NumBedroom", "Size", "Type" },
                values: new object[,]
                {
                    { 1L, 1L, 1, 1, 500.0, "Studio" },
                    { 2L, 2L, 1, 1, 650.0, "One Bedroom" },
                    { 3L, 3L, 2, 2, 900.0, "Two Bedroom" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Buildings",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "UrbanAreas",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Buildings",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Buildings",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "UrbanAreas",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "UrbanAreas",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
