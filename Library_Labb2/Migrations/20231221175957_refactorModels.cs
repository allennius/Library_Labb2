using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Labb2.Migrations
{
    /// <inheritdoc />
    public partial class refactorModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Customers_CustomerId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_libraryCards_LibraryCardId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_LibraryCardId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "LibraryCardId",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Loans",
                newName: "LibCardLibraryCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_CustomerId",
                table: "Loans",
                newName: "IX_Loans_LibCardLibraryCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_libraryCards_LibCardLibraryCardId",
                table: "Loans",
                column: "LibCardLibraryCardId",
                principalTable: "libraryCards",
                principalColumn: "LibraryCardId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_libraryCards_LibCardLibraryCardId",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "LibCardLibraryCardId",
                table: "Loans",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_LibCardLibraryCardId",
                table: "Loans",
                newName: "IX_Loans_CustomerId");

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
                name: "FK_Loans_Customers_CustomerId",
                table: "Loans",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_libraryCards_LibraryCardId",
                table: "Loans",
                column: "LibraryCardId",
                principalTable: "libraryCards",
                principalColumn: "LibraryCardId");
        }
    }
}
