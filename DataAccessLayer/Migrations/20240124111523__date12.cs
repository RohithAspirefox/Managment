using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class _date12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfJoining",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554a8f54-c054-4de6-9654-654321098765",
                columns: new[] { "ConcurrencyStamp", "DateOfJoining", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e110e20-9c60-438e-bb2c-f6f7fad6e58d", null, "AQAAAAIAAYagAAAAEIqgWAjrtEq9VQ9bcEk8wt3AnCYne4Ph3d6CBtdbk40jCCb+J6EE6DfU+cFtBFtsjg==", "ec7e17db-2832-4e3a-bc1c-19f48a6c7118" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "774a8f54-c054-4de6-9654-654321098755",
                columns: new[] { "ConcurrencyStamp", "DateOfJoining", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4cce839-239d-4314-846e-4b008ea66ddc", null, "AQAAAAIAAYagAAAAEPA1JfEzBECvFSgQmgxwMDreSSSajbHSlmBZ5cCzK6Rp8uXx+uXi1h+oIBGxK9FtkA==", "4ba90651-da4f-4fdf-bade-fdface8caf8e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "DateOfJoining", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5fb82f30-54bb-4d90-8e54-b21e15cc9390", null, "AQAAAAIAAYagAAAAEG2Mgm4xF65gshyzYyzIbJeYdNpNVxZSXbE4GRj99TRpOZnczz8ZgRVZNRTlwkAI9w==", "5528d522-91f4-429d-b2a0-1a2d22d317c9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfJoining",
                table: "AspNetUsers",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
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
    }
}
