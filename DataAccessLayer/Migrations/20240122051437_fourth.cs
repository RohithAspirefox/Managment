using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554a8f54-c054-4de6-9654-654321098765",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6d0c283-0d39-4ad4-8809-1c6a80ecf22f", "AQAAAAIAAYagAAAAEDqTXMqQ0cVJZ+n8gRQ+mBvYLmN7zpdy/vu+nT4hE8nla1XolzzF/3qlPSclmLLTPw==", "18f0bae7-ad91-4e1c-9e5c-d57863c4e83c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "774a8f54-c054-4de6-9654-654321098755",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "74182ecf-3ffa-4fa7-8c87-2bfa2446af74", "AQAAAAIAAYagAAAAEPRU7oHJ2vee8GkkOfjY9AjfgWYMNeN8lg7pXdRYKhnCSHtlaND8wMhAxWpwjAmuvw==", "7696f05d-f839-4dc2-9fbc-36dfb3cd4636" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c7a9f3b9-0afd-41b9-a5dd-62689a82060b", "AQAAAAIAAYagAAAAEK8tMBetmufpyPoNIKEVIvRwRWqKOChZF8/iAY7vAnEMUlFY2ytXLvV2OEoxW7sotg==", "8497a17d-c7bc-451c-97c9-3573b302cc5a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554a8f54-c054-4de6-9654-654321098765",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "125fd001-ee93-4671-bc8e-8e1dcdfeec92", "AQAAAAIAAYagAAAAEO2sLwMbMNx2Qh6QmhebUakSfTsQFUoqRP0kngv2Cy9tk7gEGTfMmRxoSbAe3PyYZA==", "81fde47b-2504-4511-b693-2b73aedd1c25" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "774a8f54-c054-4de6-9654-654321098755",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3af4753c-8c33-4a0b-aec4-7de7f9e2fb9c", "AQAAAAIAAYagAAAAEJS3XgBIzZTuWWzSXBV9NcWkeQ3Ywk9m9wJhjNEyoctHfHJiJjWc42iz3d3CohHVeQ==", "dab5514d-4bdb-42b4-9ac2-f327d0e23e29" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "09781078-7fbd-483c-84b5-6fdda559f8e2", "AQAAAAIAAYagAAAAEOAtEBbRlMhqNfS56dLHaa4WLyk9s9kroQ0JvC+2rpiRm/S+5n4p2gHRv5tzinzi2Q==", "aad1a593-0e6a-47e7-83ad-d4a82765e7e1" });
        }
    }
}
