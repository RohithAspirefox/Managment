using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageURl",
                table: "AspNetUsers",
                newName: "ImageURL");

            migrationBuilder.AlterColumn<string>(
                name: "ImageURL",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554a8f54-c054-4de6-9654-654321098765",
                columns: new[] { "ConcurrencyStamp", "ImageURL", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e60810ca-9106-4acb-bf82-ce891597b801", null, "AQAAAAIAAYagAAAAEHxLAAabhTZI6S7gqoVIy/7uZTomgzQWh2o2Lr/RB4p8ZVrZD/LxzvMi7MEoVmPs8A==", "b87ee6cd-1483-4ca3-a213-cf221158bf39" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "774a8f54-c054-4de6-9654-654321098755",
                columns: new[] { "ConcurrencyStamp", "ImageURL", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b227074-c35a-4d78-9a1b-468f3de0d23d", null, "AQAAAAIAAYagAAAAEP7n+NW38U/xd89JZci0cgu0xiMBtWIHfiduiz5MxaTeQDjFiXkXgZ4XI2imo5IThw==", "e8ab865f-1172-4780-a08b-066509bed756" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "ImageURL", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ec746cc2-d280-422d-948a-737a140a08fb", null, "AQAAAAIAAYagAAAAEH9ZGEbRIhuYpH5kUMq9+/NdryAHvSknL3ZmHrEkhA77JjB4K8dmVYlm/vCArzJ/cA==", "7e2df0b2-e83a-4102-beda-75a8ecadeffa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageURL",
                table: "AspNetUsers",
                newName: "ImageURl");

            migrationBuilder.AlterColumn<bool>(
                name: "ImageURl",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554a8f54-c054-4de6-9654-654321098765",
                columns: new[] { "ConcurrencyStamp", "ImageURl", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6d0c283-0d39-4ad4-8809-1c6a80ecf22f", null, "AQAAAAIAAYagAAAAEDqTXMqQ0cVJZ+n8gRQ+mBvYLmN7zpdy/vu+nT4hE8nla1XolzzF/3qlPSclmLLTPw==", "18f0bae7-ad91-4e1c-9e5c-d57863c4e83c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "774a8f54-c054-4de6-9654-654321098755",
                columns: new[] { "ConcurrencyStamp", "ImageURl", "PasswordHash", "SecurityStamp" },
                values: new object[] { "74182ecf-3ffa-4fa7-8c87-2bfa2446af74", null, "AQAAAAIAAYagAAAAEPRU7oHJ2vee8GkkOfjY9AjfgWYMNeN8lg7pXdRYKhnCSHtlaND8wMhAxWpwjAmuvw==", "7696f05d-f839-4dc2-9fbc-36dfb3cd4636" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "ImageURl", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c7a9f3b9-0afd-41b9-a5dd-62689a82060b", null, "AQAAAAIAAYagAAAAEK8tMBetmufpyPoNIKEVIvRwRWqKOChZF8/iAY7vAnEMUlFY2ytXLvV2OEoxW7sotg==", "8497a17d-c7bc-451c-97c9-3573b302cc5a" });
        }
    }
}
