using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinalProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedBuildItemSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BuildItems",
                columns: new[] { "BuildId", "ItemId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BuildItems",
                keyColumns: new[] { "BuildId", "ItemId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BuildItems",
                keyColumns: new[] { "BuildId", "ItemId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "BuildItems",
                keyColumns: new[] { "BuildId", "ItemId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "BuildItems",
                keyColumns: new[] { "BuildId", "ItemId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "BuildItems",
                keyColumns: new[] { "BuildId", "ItemId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "BuildItems",
                keyColumns: new[] { "BuildId", "ItemId" },
                keyValues: new object[] { 1, 6 });
        }
    }
}
