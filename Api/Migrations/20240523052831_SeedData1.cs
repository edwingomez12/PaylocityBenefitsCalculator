using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class SeedData1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "Salary" },
                values: new object[] { 1, new DateTime(1984, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "LeBron", "James", 75420.99m });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "Salary" },
                values: new object[] { 2, new DateTime(1999, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ja", "Morant", 92365.22m });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "Salary" },
                values: new object[] { 3, new DateTime(1963, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Michael", "Jordan", 143211.12m });

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "DateOfBirth", "EmployeeId", "FirstName", "LastName", "Relationship" },
                values: new object[] { 1, new DateTime(1998, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Spouse", "Morant", 1 });

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "DateOfBirth", "EmployeeId", "FirstName", "LastName", "Relationship" },
                values: new object[] { 2, new DateTime(2020, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Child1", "Morant", 3 });

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "DateOfBirth", "EmployeeId", "FirstName", "LastName", "Relationship" },
                values: new object[] { 3, new DateTime(2021, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Child2", "Morant", 3 });

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "DateOfBirth", "EmployeeId", "FirstName", "LastName", "Relationship" },
                values: new object[] { 4, new DateTime(1974, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "DP", "Jordan", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Dependents",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Dependents",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Dependents",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Dependents",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
