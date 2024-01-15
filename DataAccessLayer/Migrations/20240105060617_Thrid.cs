using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class Thrid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "123013f0-5201-4317-abd8-c211f91b7123", "3", "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554a8f54-c054-4de6-9654-654321098765",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9402a057-dce3-4846-803e-1b115d7128c1", "AQAAAAIAAYagAAAAEOP3CyZVzaZv+sHN2z3pNfC6yLHRFlfU/jfogJ/E1oGgwGKUtvNcYU6TZ1qSUCGSFw==", "143b2c61-3020-45df-bf41-d8ce058d09b8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e90bfb8e-913e-4f18-bc3d-a144833a00a5", "AQAAAAIAAYagAAAAEDhZI7IgmdVPzDyM/XAcLtGnFhHKbwaOj4u3JKdZRlJf2tYAviZvAz75lCyGN3BuAQ==", "6f67ee68-a9e8-47cd-9404-fb016604cda1" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "774a8f54-c054-4de6-9654-654321098755", 0, "9733440a-2a9b-4948-9215-9814781f79d9", "user@gmail.com", false, "User", null, false, null, "user@gmail.com", "User", "AQAAAAIAAYagAAAAEMiz7a3rWl38q3aGH5sabiRhF1WZuU98DPwZcs3qoZs8JNP2Aa7IRu4RW+D89dAUBA==", "987452361", false, "a9b682c3-6efc-4042-b69e-81118758ab62", false, "User" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "123013f0-5201-4317-abd8-c211f91b7123", "774a8f54-c054-4de6-9654-654321098755" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "123013f0-5201-4317-abd8-c211f91b7123", "774a8f54-c054-4de6-9654-654321098755" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "123013f0-5201-4317-abd8-c211f91b7123");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "774a8f54-c054-4de6-9654-654321098755");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554a8f54-c054-4de6-9654-654321098765",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8d07b03-34e0-4468-89a1-62f41c2af22d", "AQAAAAIAAYagAAAAEETZaCByV2cW6ewdBHIf5cD2LZvnF97vlF8jYrDa+2i8LUbJzwCl41ncenjnRMquIw==", "58fb3d3b-d63b-4a03-9252-6219e158ba76" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c269b1d2-f3f1-4179-afff-7184494095f9", "AQAAAAIAAYagAAAAEOCPfoBfSaPhQX3yKkFV/n/U7TRBJ48a4sXr3a0HlFo1hp/SzuzOf1/mcAiupHiERA==", "50f8cde5-ec20-435b-ad20-ecd6c22518ff" });
        }
    }
}