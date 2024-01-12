using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Labb2.Migrations
{
    /// <inheritdoc />
    public partial class ChangedLibraryCardColumnToOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_libraryCards_LibraryCardId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_LibraryCardId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "LibraryCardId",
                table: "Loans");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 9, 19, 14, 59, 342, DateTimeKind.Local).AddTicks(900),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 9, 16, 28, 51, 644, DateTimeKind.Local).AddTicks(556));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 9, 16, 28, 51, 644, DateTimeKind.Local).AddTicks(556),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 9, 19, 14, 59, 342, DateTimeKind.Local).AddTicks(900));

            migrationBuilder.AddColumn<int>(
                name: "LibraryCardId",
                table: "Loans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loans_LibraryCardId",
                table: "Loans",
                column: "LibraryCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_libraryCards_LibraryCardId",
                table: "Loans",
                column: "LibraryCardId",
                principalTable: "libraryCards",
                principalColumn: "LibraryCardId");
        }
    }
}
