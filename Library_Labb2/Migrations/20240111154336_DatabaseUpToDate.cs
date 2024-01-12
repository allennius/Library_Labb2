using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Labb2.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseUpToDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 11, 16, 43, 35, 810, DateTimeKind.Local).AddTicks(5883),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 9, 19, 14, 59, 342, DateTimeKind.Local).AddTicks(900));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 9, 19, 14, 59, 342, DateTimeKind.Local).AddTicks(900),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 11, 16, 43, 35, 810, DateTimeKind.Local).AddTicks(5883));
        }
    }
}
