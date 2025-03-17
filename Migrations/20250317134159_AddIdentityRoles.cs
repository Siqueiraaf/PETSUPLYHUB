using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d2d2d2c0-2a2d-4d5a-8b58-b3f60c07214a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f47b3c3f-32be-44f3-a2b5-264846b4a2ff"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("b423d454-7d7d-41eb-a3a8-abaef8359a27"), null, "Admin", "ADMIN" },
                    { new Guid("fedf884c-fc4c-4b7d-ba7c-261986329ac9"), null, "Cliente", "CLIENTE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b423d454-7d7d-41eb-a3a8-abaef8359a27"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fedf884c-fc4c-4b7d-ba7c-261986329ac9"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("d2d2d2c0-2a2d-4d5a-8b58-b3f60c07214a"), null, "Admin", "ADMIN" },
                    { new Guid("f47b3c3f-32be-44f3-a2b5-264846b4a2ff"), null, "Cliente", "CLIENTE" }
                });
        }
    }
}
