using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554a8f54-c054-4de6-9654-654321098765",
                columns: new[] { "Active", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "ae34c37e-7436-44b3-9329-67bf2e864db3", "AQAAAAIAAYagAAAAEObGtEViXCQRIXXfJYPXh4KuOuqbBTAyFq/ezqJ1/7OtjYdzMwjrL7SLvgEapd7Gww==", "bb8c3b70-56a5-4a0b-9715-8573cb3a36ed" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "774a8f54-c054-4de6-9654-654321098755",
                columns: new[] { "Active", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "6126c071-2e90-4a04-8f65-be2664d666f8", "AQAAAAIAAYagAAAAEOAfuvgE/hpqW661vyPc/yb6AAQjTfVbUVq1PftShfHlLvTi5eC/msZKrwH7iGlVdA==", "df7f3bb1-d479-4526-a89b-2afbdd207904" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "Active", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "e4282611-4d80-4429-9979-ce2f4efbcdc7", "AQAAAAIAAYagAAAAEMQL16jtlBa4qQLAGvfyPCimWFt9nj6Rn6ZRJ9pZpe51feFbSSQoNNF6GCDcEMStDg==", "ef3567c4-8f2c-4569-8a93-32fb64d01ecf" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554a8f54-c054-4de6-9654-654321098765",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc966125-2c1d-4bc9-8e0b-4922dbb980cb", "AQAAAAIAAYagAAAAEJcRzqi3rIAIhCCk7w3lBwx+rI5x5mJNkkOWLeAIlRgeqTxx1rmI/aKqW2rvXtzJmA==", "222e5c14-b6c1-4a1d-aa9d-b7393a3b9d0f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "774a8f54-c054-4de6-9654-654321098755",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e22311a-e975-4ec8-a747-b9b6a3a862a6", "AQAAAAIAAYagAAAAEMfAtR1MBZ1Uq5q2jPMsZ6jL/UPeJH0iAzvPqBXiia6bd/328RdpmNhkto8W89l8pw==", "31ea5d82-5021-4086-96f6-e31eb71b8a31" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a53370d3-a118-4cd0-ad8d-c3597235e2de", "AQAAAAIAAYagAAAAEBXwW6xfQu08aj9Y81KUuZ/LIAJolEuHSeb3haI/E+2cM4yPox0l/Hh8VoU2g8d3dA==", "c46d6276-cb7a-496a-829b-5da2af52ca67" });
        }
    }
}
