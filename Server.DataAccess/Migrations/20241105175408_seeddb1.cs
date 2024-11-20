using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seeddb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "NumBathroom", "NumBedroom", "Size", "Type" },
                values: new object[] { 2, 2, 75.0, "One Bedroom" });

            migrationBuilder.UpdateData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "NumBedroom", "Size", "Type" },
                values: new object[] { 3, 100.0, "Studio" });

            migrationBuilder.UpdateData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "NumBedroom", "Size", "Type" },
                values: new object[] { 1, 50.0, "Penthouse" });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Available", "BuildingId" },
                values: new object[] { "No", 2L });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Available", "BuildingId" },
                values: new object[] { "Yes", 3L });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Available", "BuildingId", "RoomNumber" },
                values: new object[] { "No", 1L, 103 });

            migrationBuilder.InsertData(
                table: "Apartments",
                columns: new[] { "Id", "Available", "BuildingId", "RoomNumber" },
                values: new object[,]
                {
                    { 4L, "Yes", 2L, 104 },
                    { 5L, "No", 3L, 105 },
                    { 6L, "Yes", 1L, 106 },
                    { 7L, "No", 2L, 107 },
                    { 8L, "Yes", 3L, 108 },
                    { 9L, "No", 1L, 109 },
                    { 10L, "Yes", 2L, 110 },
                    { 11L, "No", 3L, 111 },
                    { 12L, "Yes", 1L, 112 },
                    { 13L, "No", 2L, 113 },
                    { 14L, "Yes", 3L, 114 },
                    { 15L, "No", 1L, 115 },
                    { 16L, "Yes", 2L, 116 },
                    { 17L, "No", 3L, 117 },
                    { 18L, "Yes", 1L, 118 },
                    { 19L, "No", 2L, 119 },
                    { 20L, "Yes", 3L, 120 },
                    { 21L, "No", 1L, 121 },
                    { 22L, "Yes", 2L, 122 },
                    { 23L, "No", 3L, 123 },
                    { 24L, "Yes", 1L, 124 },
                    { 25L, "No", 2L, 125 },
                    { 26L, "Yes", 3L, 126 },
                    { 27L, "No", 1L, 127 },
                    { 28L, "Yes", 2L, 128 },
                    { 29L, "No", 3L, 129 },
                    { 30L, "Yes", 1L, 130 },
                    { 31L, "No", 2L, 131 },
                    { 32L, "Yes", 3L, 132 },
                    { 33L, "No", 1L, 133 },
                    { 34L, "Yes", 2L, 134 },
                    { 35L, "No", 3L, 135 },
                    { 36L, "Yes", 1L, 136 },
                    { 37L, "No", 2L, 137 },
                    { 38L, "Yes", 3L, 138 },
                    { 39L, "No", 1L, 139 },
                    { 40L, "Yes", 2L, 140 },
                    { 41L, "No", 3L, 141 },
                    { 42L, "Yes", 1L, 142 },
                    { 43L, "No", 2L, 143 },
                    { 44L, "Yes", 3L, 144 },
                    { 45L, "No", 1L, 145 },
                    { 46L, "Yes", 2L, 146 },
                    { 47L, "No", 3L, 147 },
                    { 48L, "Yes", 1L, 148 },
                    { 49L, "No", 2L, 149 },
                    { 50L, "Yes", 3L, 150 },
                    { 51L, "No", 1L, 151 },
                    { 52L, "Yes", 2L, 152 },
                    { 53L, "No", 3L, 153 },
                    { 54L, "Yes", 1L, 154 },
                    { 55L, "No", 2L, 155 },
                    { 56L, "Yes", 3L, 156 },
                    { 57L, "No", 1L, 157 },
                    { 58L, "Yes", 2L, 158 },
                    { 59L, "No", 3L, 159 },
                    { 60L, "Yes", 1L, 160 },
                    { 61L, "No", 2L, 161 },
                    { 62L, "Yes", 3L, 162 },
                    { 63L, "No", 1L, 163 },
                    { 64L, "Yes", 2L, 164 },
                    { 65L, "No", 3L, 165 },
                    { 66L, "Yes", 1L, 166 },
                    { 67L, "No", 2L, 167 },
                    { 68L, "Yes", 3L, 168 },
                    { 69L, "No", 1L, 169 },
                    { 70L, "Yes", 2L, 170 },
                    { 71L, "No", 3L, 171 },
                    { 72L, "Yes", 1L, 172 },
                    { 73L, "No", 2L, 173 },
                    { 74L, "Yes", 3L, 174 },
                    { 75L, "No", 1L, 175 },
                    { 76L, "Yes", 2L, 176 },
                    { 77L, "No", 3L, 177 },
                    { 78L, "Yes", 1L, 178 },
                    { 79L, "No", 2L, 179 },
                    { 80L, "Yes", 3L, 180 },
                    { 81L, "No", 1L, 181 },
                    { 82L, "Yes", 2L, 182 },
                    { 83L, "No", 3L, 183 },
                    { 84L, "Yes", 1L, 184 },
                    { 85L, "No", 2L, 185 },
                    { 86L, "Yes", 3L, 186 },
                    { 87L, "No", 1L, 187 },
                    { 88L, "Yes", 2L, 188 },
                    { 89L, "No", 3L, 189 },
                    { 90L, "Yes", 1L, 190 },
                    { 91L, "No", 2L, 191 },
                    { 92L, "Yes", 3L, 192 },
                    { 93L, "No", 1L, 193 },
                    { 94L, "Yes", 2L, 194 },
                    { 95L, "No", 3L, 195 },
                    { 96L, "Yes", 1L, 196 },
                    { 97L, "No", 2L, 197 },
                    { 98L, "Yes", 3L, 198 },
                    { 99L, "No", 1L, 199 },
                    { 100L, "Yes", 2L, 200 }
                });

            migrationBuilder.InsertData(
                table: "ApartmentDetails",
                columns: new[] { "Id", "ApartmentId", "NumBathroom", "NumBedroom", "Size", "Type" },
                values: new object[,]
                {
                    { 4L, 4L, 1, 2, 75.0, "Studio" },
                    { 5L, 5L, 2, 3, 100.0, "One Bedroom" },
                    { 6L, 6L, 1, 1, 50.0, "Penthouse" },
                    { 7L, 7L, 2, 2, 75.0, "One Bedroom" },
                    { 8L, 8L, 1, 3, 100.0, "Studio" },
                    { 9L, 9L, 2, 1, 50.0, "Penthouse" },
                    { 10L, 10L, 1, 2, 75.0, "Studio" },
                    { 11L, 11L, 2, 3, 100.0, "One Bedroom" },
                    { 12L, 12L, 1, 1, 50.0, "Penthouse" },
                    { 13L, 13L, 2, 2, 75.0, "One Bedroom" },
                    { 14L, 14L, 1, 3, 100.0, "Studio" },
                    { 15L, 15L, 2, 1, 50.0, "Penthouse" },
                    { 16L, 16L, 1, 2, 75.0, "Studio" },
                    { 17L, 17L, 2, 3, 100.0, "One Bedroom" },
                    { 18L, 18L, 1, 1, 50.0, "Penthouse" },
                    { 19L, 19L, 2, 2, 75.0, "One Bedroom" },
                    { 20L, 20L, 1, 3, 100.0, "Studio" },
                    { 21L, 21L, 2, 1, 50.0, "Penthouse" },
                    { 22L, 22L, 1, 2, 75.0, "Studio" },
                    { 23L, 23L, 2, 3, 100.0, "One Bedroom" },
                    { 24L, 24L, 1, 1, 50.0, "Penthouse" },
                    { 25L, 25L, 2, 2, 75.0, "One Bedroom" },
                    { 26L, 26L, 1, 3, 100.0, "Studio" },
                    { 27L, 27L, 2, 1, 50.0, "Penthouse" },
                    { 28L, 28L, 1, 2, 75.0, "Studio" },
                    { 29L, 29L, 2, 3, 100.0, "One Bedroom" },
                    { 30L, 30L, 1, 1, 50.0, "Penthouse" },
                    { 31L, 31L, 2, 2, 75.0, "One Bedroom" },
                    { 32L, 32L, 1, 3, 100.0, "Studio" },
                    { 33L, 33L, 2, 1, 50.0, "Penthouse" },
                    { 34L, 34L, 1, 2, 75.0, "Studio" },
                    { 35L, 35L, 2, 3, 100.0, "One Bedroom" },
                    { 36L, 36L, 1, 1, 50.0, "Penthouse" },
                    { 37L, 37L, 2, 2, 75.0, "One Bedroom" },
                    { 38L, 38L, 1, 3, 100.0, "Studio" },
                    { 39L, 39L, 2, 1, 50.0, "Penthouse" },
                    { 40L, 40L, 1, 2, 75.0, "Studio" },
                    { 41L, 41L, 2, 3, 100.0, "One Bedroom" },
                    { 42L, 42L, 1, 1, 50.0, "Penthouse" },
                    { 43L, 43L, 2, 2, 75.0, "One Bedroom" },
                    { 44L, 44L, 1, 3, 100.0, "Studio" },
                    { 45L, 45L, 2, 1, 50.0, "Penthouse" },
                    { 46L, 46L, 1, 2, 75.0, "Studio" },
                    { 47L, 47L, 2, 3, 100.0, "One Bedroom" },
                    { 48L, 48L, 1, 1, 50.0, "Penthouse" },
                    { 49L, 49L, 2, 2, 75.0, "One Bedroom" },
                    { 50L, 50L, 1, 3, 100.0, "Studio" },
                    { 51L, 51L, 2, 1, 50.0, "Penthouse" },
                    { 52L, 52L, 1, 2, 75.0, "Studio" },
                    { 53L, 53L, 2, 3, 100.0, "One Bedroom" },
                    { 54L, 54L, 1, 1, 50.0, "Penthouse" },
                    { 55L, 55L, 2, 2, 75.0, "One Bedroom" },
                    { 56L, 56L, 1, 3, 100.0, "Studio" },
                    { 57L, 57L, 2, 1, 50.0, "Penthouse" },
                    { 58L, 58L, 1, 2, 75.0, "Studio" },
                    { 59L, 59L, 2, 3, 100.0, "One Bedroom" },
                    { 60L, 60L, 1, 1, 50.0, "Penthouse" },
                    { 61L, 61L, 2, 2, 75.0, "One Bedroom" },
                    { 62L, 62L, 1, 3, 100.0, "Studio" },
                    { 63L, 63L, 2, 1, 50.0, "Penthouse" },
                    { 64L, 64L, 1, 2, 75.0, "Studio" },
                    { 65L, 65L, 2, 3, 100.0, "One Bedroom" },
                    { 66L, 66L, 1, 1, 50.0, "Penthouse" },
                    { 67L, 67L, 2, 2, 75.0, "One Bedroom" },
                    { 68L, 68L, 1, 3, 100.0, "Studio" },
                    { 69L, 69L, 2, 1, 50.0, "Penthouse" },
                    { 70L, 70L, 1, 2, 75.0, "Studio" },
                    { 71L, 71L, 2, 3, 100.0, "One Bedroom" },
                    { 72L, 72L, 1, 1, 50.0, "Penthouse" },
                    { 73L, 73L, 2, 2, 75.0, "One Bedroom" },
                    { 74L, 74L, 1, 3, 100.0, "Studio" },
                    { 75L, 75L, 2, 1, 50.0, "Penthouse" },
                    { 76L, 76L, 1, 2, 75.0, "Studio" },
                    { 77L, 77L, 2, 3, 100.0, "One Bedroom" },
                    { 78L, 78L, 1, 1, 50.0, "Penthouse" },
                    { 79L, 79L, 2, 2, 75.0, "One Bedroom" },
                    { 80L, 80L, 1, 3, 100.0, "Studio" },
                    { 81L, 81L, 2, 1, 50.0, "Penthouse" },
                    { 82L, 82L, 1, 2, 75.0, "Studio" },
                    { 83L, 83L, 2, 3, 100.0, "One Bedroom" },
                    { 84L, 84L, 1, 1, 50.0, "Penthouse" },
                    { 85L, 85L, 2, 2, 75.0, "One Bedroom" },
                    { 86L, 86L, 1, 3, 100.0, "Studio" },
                    { 87L, 87L, 2, 1, 50.0, "Penthouse" },
                    { 88L, 88L, 1, 2, 75.0, "Studio" },
                    { 89L, 89L, 2, 3, 100.0, "One Bedroom" },
                    { 90L, 90L, 1, 1, 50.0, "Penthouse" },
                    { 91L, 91L, 2, 2, 75.0, "One Bedroom" },
                    { 92L, 92L, 1, 3, 100.0, "Studio" },
                    { 93L, 93L, 2, 1, 50.0, "Penthouse" },
                    { 94L, 94L, 1, 2, 75.0, "Studio" },
                    { 95L, 95L, 2, 3, 100.0, "One Bedroom" },
                    { 96L, 96L, 1, 1, 50.0, "Penthouse" },
                    { 97L, 97L, 2, 2, 75.0, "One Bedroom" },
                    { 98L, 98L, 1, 3, 100.0, "Studio" },
                    { 99L, 99L, 2, 1, 50.0, "Penthouse" },
                    { 100L, 100L, 1, 2, 75.0, "Studio" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 54L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 55L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 57L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 58L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 59L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 60L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 61L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 63L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 65L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 66L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 67L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 68L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 69L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 71L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 73L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 75L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 76L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 77L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 78L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 79L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 80L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 81L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 82L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 83L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 84L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 85L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 86L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 87L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 88L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 89L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 90L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 91L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 92L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 93L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 94L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 95L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 96L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 97L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 98L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 99L);

            migrationBuilder.DeleteData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 54L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 55L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 57L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 58L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 59L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 60L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 61L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 63L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 65L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 66L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 67L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 68L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 69L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 71L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 73L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 75L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 76L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 77L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 78L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 79L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 80L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 81L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 82L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 83L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 84L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 85L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 86L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 87L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 88L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 89L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 90L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 91L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 92L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 93L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 94L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 95L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 96L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 97L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 98L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 99L);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 100L);

            migrationBuilder.UpdateData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "NumBathroom", "NumBedroom", "Size", "Type" },
                values: new object[] { 1, 1, 500.0, "Studio" });

            migrationBuilder.UpdateData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "NumBedroom", "Size", "Type" },
                values: new object[] { 1, 650.0, "One Bedroom" });

            migrationBuilder.UpdateData(
                table: "ApartmentDetails",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "NumBedroom", "Size", "Type" },
                values: new object[] { 2, 900.0, "Two Bedroom" });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Available", "BuildingId" },
                values: new object[] { "Yes", 1L });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Available", "BuildingId" },
                values: new object[] { "No", 1L });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Available", "BuildingId", "RoomNumber" },
                values: new object[] { "Yes", 2L, 201 });
        }
    }
}
