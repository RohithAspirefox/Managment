using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacebookURL",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GithubURL",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramURL",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterURL",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554a8f54-c054-4de6-9654-654321098765",
                columns: new[] { "ConcurrencyStamp", "FacebookURL", "GithubURL", "InstagramURL", "PasswordHash", "SecurityStamp", "TwitterURL" },
                values: new object[] { "62dbc568-f4b9-4751-83b5-1a9948714a3d", null, null, null, "AQAAAAIAAYagAAAAECFk4phkc2o/mEMM8ASq6cBec1nYhhtXZEe51nYGyZggDnfYZqjTw+KS7Pmm9X7aoA==", "8a0cc561-e911-4302-8d27-93fe13f7877c", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "774a8f54-c054-4de6-9654-654321098755",
                columns: new[] { "ConcurrencyStamp", "FacebookURL", "GithubURL", "InstagramURL", "PasswordHash", "SecurityStamp", "TwitterURL" },
                values: new object[] { "b87989e4-0ff8-48fe-a9c4-52e0cdb21d76", null, null, null, "AQAAAAIAAYagAAAAEPprCNrHD03MgQ3Wi/uwpXbCRD7qV0y+7YGPYh8Y5QAzOLMd1T6t5esAbZEPPDPCkw==", "3c35a42a-5372-414c-a2b4-39c75f0a1fef", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "FacebookURL", "GithubURL", "InstagramURL", "PasswordHash", "SecurityStamp", "TwitterURL" },
                values: new object[] { "7b197553-8a73-4558-8fd4-cf8e516f5ca2", null, null, null, "AQAAAAIAAYagAAAAEPDkTATCE8tLaDFJ/f7wCdVZF3CGsTWdllFzdWaSxvA9+OlpPyQKTtiz4HzPAFBArw==", "8fa8cb20-4603-4e36-88dd-39cb582d54b1", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookURL",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GithubURL",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InstagramURL",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TwitterURL",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554a8f54-c054-4de6-9654-654321098765",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "802f42cc-f913-4c5c-985a-0432f49588fe", "AQAAAAIAAYagAAAAEA5mnjhtnS2AVbKC/Qx7qoei+zHuf9D6PjvRFCAABZid50ssbpWcqYyNYKHYDdJH9A==", "5867991f-fc7e-4139-9527-43a12c0e34be" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "774a8f54-c054-4de6-9654-654321098755",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a8ebfe0d-52de-4811-b716-d7118394f157", "AQAAAAIAAYagAAAAEHmer6ZOqQ1FQzydS3oEL6bR/YaWKx5RZWN85h6xuwSth2VwVQR2opYTFrFyDdYx8Q==", "b292f092-0cf1-42b7-980a-5705523453ac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1c3bc27-91b2-4590-b8c2-73af074d131e", "AQAAAAIAAYagAAAAEJD+hei7IriUAY6oyq7TxqGjUSX0mxk1A62fXSBg5kzY6KaKUyr2nfogO/EPi7ETgQ==", "69d4f16b-cb75-4b10-97ba-f4b8e0519839" });
        }
    }
}
