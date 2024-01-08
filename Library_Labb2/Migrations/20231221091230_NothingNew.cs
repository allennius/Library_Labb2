using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Labb2.Migrations
{
    /// <inheritdoc />
    public partial class NothingNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBook_Authors_AuthorID",
                table: "AuthorBook");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "AuthorID",
                table: "AuthorBook",
                newName: "AuthorsAuthorID");

            migrationBuilder.AlterColumn<float>(
                name: "Grade",
                table: "Rating",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBook_Authors_AuthorsAuthorID",
                table: "AuthorBook",
                column: "AuthorsAuthorID",
                principalTable: "Authors",
                principalColumn: "AuthorID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBook_Authors_AuthorsAuthorID",
                table: "AuthorBook");

            migrationBuilder.RenameColumn(
                name: "AuthorsAuthorID",
                table: "AuthorBook",
                newName: "AuthorID");

            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "Rating",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBook_Authors_AuthorID",
                table: "AuthorBook",
                column: "AuthorID",
                principalTable: "Authors",
                principalColumn: "AuthorID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
