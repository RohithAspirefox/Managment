using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class _date : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfJoining",
                table: "AspNetUsers",
                type: "date",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554a8f54-c054-4de6-9654-654321098765",
                columns: new[] { "ConcurrencyStamp", "DateOfJoining", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b755e202-b515-4eaa-88a3-89fe13832dbe", null, "AQAAAAIAAYagAAAAEBLKUjyA9jYyBbJVTfOJQsaSCAQ8BaSWslu4R0QcQJ0OBTDRIMGs40L7LuxBYD2LZg==", "fb452a2d-5d86-4c1d-878f-4c68a4de70c6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "774a8f54-c054-4de6-9654-654321098755",
                columns: new[] { "ConcurrencyStamp", "DateOfJoining", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f4d757db-e7a1-4ea0-a4da-aeeda434778f", null, "AQAAAAIAAYagAAAAECC9LITKyRghUWogJNWCwdWcBl4FPV+na9dM9JS116U+I+HIoPWyxPHnj6s0bIfGkg==", "0c370148-0b0e-4c1b-a510-d8f9f4abb557" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "DateOfJoining", "PasswordHash", "SecurityStamp" },
                values: new object[] { "abf3c0fe-70ab-4dc5-a573-e960f8307fe3", null, "AQAAAAIAAYagAAAAENGvZtBtDNbObyOfOaiNk2aaPEiEGlq/dWNn+MPic+tbPHIcVXRts6diCT/Bp9MH5g==", "95f2e4c6-c12e-4bff-81eb-457df6eb944e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DateOfJoining",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554a8f54-c054-4de6-9654-654321098765",
                columns: new[] { "ConcurrencyStamp", "DateOfJoining", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8018ffe6-9de2-4691-b7f0-83011b7418ff", null, "AQAAAAIAAYagAAAAEPgc7Ld9sM92GPfT/V89rNxQCORTkdgHx1Aof6GTB+YCUwNaQsHH6ptXFcvyp16okQ==", "87fdda2a-a9aa-4afe-bc57-298e7fb1bb2f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "774a8f54-c054-4de6-9654-654321098755",
                columns: new[] { "ConcurrencyStamp", "DateOfJoining", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f54ca0a-d91c-4862-b73a-22ffa18b54c4", null, "AQAAAAIAAYagAAAAEBLd0hIjqRYbU8d607393WMa3TbWxYfLoAJDlG+RYjDs7TP+nRUfrdAT8dQZQlL3Qw==", "6851b62a-d45a-4e60-a703-a56dc8217340" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "DateOfJoining", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b88fd34-3ef3-4035-a410-fbf40d297b52", null, "AQAAAAIAAYagAAAAENfkLUMhtlSHNGtTuTS6OtLc5ozqJwr5mH4IG6+e3rSXnzduis0haoQBW6RtjeBm9Q==", "fce5f0ac-8cb2-4cc3-838d-172493654dd5" });
        }
    }
}
