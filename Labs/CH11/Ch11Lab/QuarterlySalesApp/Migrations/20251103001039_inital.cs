using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuarterlySalesApp.Migrations
{
    /// <inheritdoc />
    public partial class inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfHire = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SalesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quarter = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SalesId);
                    table.ForeignKey(
                        name: "FK_Sales_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DOB", "DateOfHire", "FirstName", "LastName", "ManagerId" },
                values: new object[,]
                {
                    { 1, new DateTime(1956, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1995, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", "Doe", 0 },
                    { 2, new DateTime(1985, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2012, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane", "Smith", 1 },
                    { 3, new DateTime(1990, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Michael", "Johnson", 1 }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "SalesId", "Amount", "EmployeeId", "Quarter", "Year" },
                values: new object[,]
                {
                    { 1, 15000.0, 2, 3, 2023 },
                    { 2, 20000.0, 1, 4, 2023 },
                    { 3, 18000.0, 3, 1, 2023 },
                    { 4, 22000.0, 3, 2, 2023 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_EmployeeId",
                table: "Sales",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
