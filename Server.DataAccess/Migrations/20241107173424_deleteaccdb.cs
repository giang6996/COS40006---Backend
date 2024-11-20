using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class deleteaccdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentDetails_Documents_DocumentId",
                table: "DocumentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Accounts_AccountId",
                table: "Documents");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentDetails_Documents_DocumentId",
                table: "DocumentDetails",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Accounts_AccountId",
                table: "Documents",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentDetails_Documents_DocumentId",
                table: "DocumentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Accounts_AccountId",
                table: "Documents");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "Status", "TenantId" },
                values: new object[,]
                {
                    { 1L, "test001@gmail.com", "test", "001", "$2a$11$eaCV0/gsB6YuF3e86QZ2CeUAb2dK7L1rSfuNxNiVPyGpkEVqzj6s.", null, "Active", 1L },
                    { 2L, "test002@gmail.com", "test", "002", "$2a$11$eaCV0/gsB6YuF3e86QZ2CeUAb2dK7L1rSfuNxNiVPyGpkEVqzj6s.", null, "Pending", 1L },
                    { 3L, "test003@gmail.com", "test", "003", "$2a$11$eaCV0/gsB6YuF3e86QZ2CeUAb2dK7L1rSfuNxNiVPyGpkEVqzj6s.", null, "Pending", 2L },
                    { 4L, "test004@gmail.com", "test", "004", "$2a$11$eaCV0/gsB6YuF3e86QZ2CeUAb2dK7L1rSfuNxNiVPyGpkEVqzj6s.", null, "Pending", 3L },
                    { 5L, "test005@gmail.com", "test", "005", "$2a$11$eaCV0/gsB6YuF3e86QZ2CeUAb2dK7L1rSfuNxNiVPyGpkEVqzj6s.", null, "Pending", 5L }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentDetails_Documents_DocumentId",
                table: "DocumentDetails",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Accounts_AccountId",
                table: "Documents",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
