using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class seventh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554a8f54-c054-4de6-9654-654321098765",
                columns: new[] { "Active", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "Yes", "8018ffe6-9de2-4691-b7f0-83011b7418ff", "AQAAAAIAAYagAAAAEPgc7Ld9sM92GPfT/V89rNxQCORTkdgHx1Aof6GTB+YCUwNaQsHH6ptXFcvyp16okQ==", "87fdda2a-a9aa-4afe-bc57-298e7fb1bb2f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "774a8f54-c054-4de6-9654-654321098755",
                columns: new[] { "Active", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "Yes", "3f54ca0a-d91c-4862-b73a-22ffa18b54c4", "AQAAAAIAAYagAAAAEBLd0hIjqRYbU8d607393WMa3TbWxYfLoAJDlG+RYjDs7TP+nRUfrdAT8dQZQlL3Qw==", "6851b62a-d45a-4e60-a703-a56dc8217340" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "Active", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "Yes", "6b88fd34-3ef3-4035-a410-fbf40d297b52", "AQAAAAIAAYagAAAAENfkLUMhtlSHNGtTuTS6OtLc5ozqJwr5mH4IG6+e3rSXnzduis0haoQBW6RtjeBm9Q==", "fce5f0ac-8cb2-4cc3-838d-172493654dd5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554a8f54-c054-4de6-9654-654321098765",
                columns: new[] { "Active", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "0c446e74-e31e-4064-95b5-9f56c3076f88", "AQAAAAIAAYagAAAAEPTzdVuhQgN40vGvV6upKsWvKQz2zLQUbiOkVeUXpNyG5SOaPvJM2K9wVbQZXUvdlQ==", "aea3424c-9dad-45fc-a6b4-3c312b38dfb2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "774a8f54-c054-4de6-9654-654321098755",
                columns: new[] { "Active", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "fa438951-cacc-4a83-959b-ae54efed318b", "AQAAAAIAAYagAAAAEPXnzNrfze/ry1eAJ60aP3Da268ei/PNSP/T5Qcbcs/MXdyhU4j1cKThyVLxKv8FOg==", "416288de-7312-48d6-a996-6731fff3c8cc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "Active", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "a40ab563-587e-40d6-8bf8-f5e4bbd40dd3", "AQAAAAIAAYagAAAAEM7gDzgZ2LeXOP7xkrwdwNj8vlTzsDgr0uP3BIx8hiTJ6WBTE3YJ1qdWeOl5YcDLIA==", "98ceae0f-9283-45a7-9cdb-3d2ce1a97ac6" });
        }
    }
}
