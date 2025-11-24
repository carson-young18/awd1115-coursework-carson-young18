using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedBuildSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Builds",
                columns: new[] { "BuildId", "CategoryId", "ChampionId", "ImageUrl", "Name", "TotalCost", "UserId" },
                values: new object[] { 1, 1, 1, "https://via.placeholder.com/150", "Yorick Top", 24000, "8ba42c86-75f2-43b3-8efa-13b2f3115e0e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Builds",
                keyColumn: "BuildId",
                keyValue: 1);
        }
    }
}
