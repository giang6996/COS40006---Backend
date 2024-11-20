using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class deleteaccdb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessTokens_RefreshTokens_RtId",
                table: "AccessTokens");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessTokens_RefreshTokens_RtId",
                table: "AccessTokens",
                column: "RtId",
                principalTable: "RefreshTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessTokens_RefreshTokens_RtId",
                table: "AccessTokens");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessTokens_RefreshTokens_RtId",
                table: "AccessTokens",
                column: "RtId",
                principalTable: "RefreshTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
