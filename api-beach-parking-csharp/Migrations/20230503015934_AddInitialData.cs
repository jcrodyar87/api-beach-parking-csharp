using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api_beach_parking_csharp.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "cars",
                columns: new[] { "id", "code", "color", "creation_date", "model", "name", "seats_quantity", "status", "updated_date" },
                values: new object[,]
                {
                    { 1, "REF-717", "azul", new DateTime(2023, 5, 2, 20, 59, 34, 634, DateTimeKind.Local).AddTicks(1879), "Wolkswagen", "Wolkswagen Gol Azul", 5, 1, new DateTime(2023, 5, 2, 20, 59, 34, 634, DateTimeKind.Local).AddTicks(1881) },
                    { 2, "A1A-123", "rojo", new DateTime(2023, 5, 2, 20, 59, 34, 634, DateTimeKind.Local).AddTicks(1883), "Nissan", "Nissan 2023", 5, 1, new DateTime(2023, 5, 2, 20, 59, 34, 634, DateTimeKind.Local).AddTicks(1884) }
                });

            migrationBuilder.InsertData(
                table: "clients",
                columns: new[] { "id", "creation_date", "email", "first_name", "last_name", "phone", "status", "updated_date" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 2, 20, 59, 34, 634, DateTimeKind.Local).AddTicks(1668), "jcry87@gmail.com", "Juan", "Rodriguez", "999999999", 1, new DateTime(2023, 5, 2, 20, 59, 34, 634, DateTimeKind.Local).AddTicks(1680) },
                    { 2, new DateTime(2023, 5, 2, 20, 59, 34, 634, DateTimeKind.Local).AddTicks(1683), "jcry1987@gmail.com", "Carlos", "Rodriguez", "909999999", 1, new DateTime(2023, 5, 2, 20, 59, 34, 634, DateTimeKind.Local).AddTicks(1684) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 2);
        }
    }
}
