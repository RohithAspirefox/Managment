using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class sixth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554a8f54-c054-4de6-9654-654321098765",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c446e74-e31e-4064-95b5-9f56c3076f88", "AQAAAAIAAYagAAAAEPTzdVuhQgN40vGvV6upKsWvKQz2zLQUbiOkVeUXpNyG5SOaPvJM2K9wVbQZXUvdlQ==", "aea3424c-9dad-45fc-a6b4-3c312b38dfb2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "774a8f54-c054-4de6-9654-654321098755",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fa438951-cacc-4a83-959b-ae54efed318b", "AQAAAAIAAYagAAAAEPXnzNrfze/ry1eAJ60aP3Da268ei/PNSP/T5Qcbcs/MXdyhU4j1cKThyVLxKv8FOg==", "416288de-7312-48d6-a996-6731fff3c8cc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a40ab563-587e-40d6-8bf8-f5e4bbd40dd3", "AQAAAAIAAYagAAAAEM7gDzgZ2LeXOP7xkrwdwNj8vlTzsDgr0uP3BIx8hiTJ6WBTE3YJ1qdWeOl5YcDLIA==", "98ceae0f-9283-45a7-9cdb-3d2ce1a97ac6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554a8f54-c054-4de6-9654-654321098765",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e60810ca-9106-4acb-bf82-ce891597b801", "AQAAAAIAAYagAAAAEHxLAAabhTZI6S7gqoVIy/7uZTomgzQWh2o2Lr/RB4p8ZVrZD/LxzvMi7MEoVmPs8A==", "b87ee6cd-1483-4ca3-a213-cf221158bf39" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "774a8f54-c054-4de6-9654-654321098755",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b227074-c35a-4d78-9a1b-468f3de0d23d", "AQAAAAIAAYagAAAAEP7n+NW38U/xd89JZci0cgu0xiMBtWIHfiduiz5MxaTeQDjFiXkXgZ4XI2imo5IThw==", "e8ab865f-1172-4780-a08b-066509bed756" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ec746cc2-d280-422d-948a-737a140a08fb", "AQAAAAIAAYagAAAAEH9ZGEbRIhuYpH5kUMq9+/NdryAHvSknL3ZmHrEkhA77JjB4K8dmVYlm/vCArzJ/cA==", "7e2df0b2-e83a-4102-beda-75a8ecadeffa" });
        }
    }
}
