using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554a8f54-c054-4de6-9654-654321098765",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ae34c37e-7436-44b3-9329-67bf2e864db3", "AQAAAAIAAYagAAAAEObGtEViXCQRIXXfJYPXh4KuOuqbBTAyFq/ezqJ1/7OtjYdzMwjrL7SLvgEapd7Gww==", "bb8c3b70-56a5-4a0b-9715-8573cb3a36ed" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "774a8f54-c054-4de6-9654-654321098755",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6126c071-2e90-4a04-8f65-be2664d666f8", "AQAAAAIAAYagAAAAEOAfuvgE/hpqW661vyPc/yb6AAQjTfVbUVq1PftShfHlLvTi5eC/msZKrwH7iGlVdA==", "df7f3bb1-d479-4526-a89b-2afbdd207904" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e4282611-4d80-4429-9979-ce2f4efbcdc7", "AQAAAAIAAYagAAAAEMQL16jtlBa4qQLAGvfyPCimWFt9nj6Rn6ZRJ9pZpe51feFbSSQoNNF6GCDcEMStDg==", "ef3567c4-8f2c-4569-8a93-32fb64d01ecf" });
        }
    }
}
