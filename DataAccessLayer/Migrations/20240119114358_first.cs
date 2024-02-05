using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateOfJoining",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554a8f54-c054-4de6-9654-654321098765",
                columns: new[] { "Address", "City", "ConcurrencyStamp", "DateOfJoining", "Gender", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, null, "dc966125-2c1d-4bc9-8e0b-4922dbb980cb", null, null, "AQAAAAIAAYagAAAAEJcRzqi3rIAIhCCk7w3lBwx+rI5x5mJNkkOWLeAIlRgeqTxx1rmI/aKqW2rvXtzJmA==", "222e5c14-b6c1-4a1d-aa9d-b7393a3b9d0f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "774a8f54-c054-4de6-9654-654321098755",
                columns: new[] { "Address", "City", "ConcurrencyStamp", "DateOfJoining", "Gender", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, null, "5e22311a-e975-4ec8-a747-b9b6a3a862a6", null, null, "AQAAAAIAAYagAAAAEMfAtR1MBZ1Uq5q2jPMsZ6jL/UPeJH0iAzvPqBXiia6bd/328RdpmNhkto8W89l8pw==", "31ea5d82-5021-4086-96f6-e31eb71b8a31" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "Address", "City", "ConcurrencyStamp", "DateOfJoining", "Gender", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, null, "a53370d3-a118-4cd0-ad8d-c3597235e2de", null, null, "AQAAAAIAAYagAAAAEBXwW6xfQu08aj9Y81KUuZ/LIAJolEuHSeb3haI/E+2cM4yPox0l/Hh8VoU2g8d3dA==", "c46d6276-cb7a-496a-829b-5da2af52ca67" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateOfJoining",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554a8f54-c054-4de6-9654-654321098765",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1cf9ef0-9a75-426c-981a-60710fe2f47e", "AQAAAAIAAYagAAAAEJk86WZliSBZzx3gwH4vc+HA4HjgvLeSBfWbK9UddAQq2uquJ80usrYBKqyHssg44A==", "856ef372-9ec9-45f8-a870-65a80c66a0d8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "774a8f54-c054-4de6-9654-654321098755",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "004b5494-10e9-4da5-82b5-a8e72e073496", "AQAAAAIAAYagAAAAEFdStEwZ0OQDygx2EZjp87x1N+Uz5JNTWvTBnsNutjTeYoMgbuNWqMFPsn9azEgvKw==", "ef012a84-2e2e-41c9-af91-d2558c1bf981" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7319ad70-9738-4a65-8bac-902376c2fc18", "AQAAAAIAAYagAAAAEPfFlgN2QSXvr5+miRuNQ6fAm81m5EDJsRJlmhaKmbmyRRckyuls49M3sj0beDEPXw==", "fad72171-acf8-4170-a45a-a75fa79e69ad" });
        }
    }
}
