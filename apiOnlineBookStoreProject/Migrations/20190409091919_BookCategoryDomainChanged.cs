using Microsoft.EntityFrameworkCore.Migrations;

namespace apiOnlineBookStoreProject.Migrations
{
    public partial class BookCategoryDomainChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookCategoryName",
                table: "BookCategories",
                newName: "BookCategoryNmae");

            migrationBuilder.AlterColumn<string>(
                name: "BookCategoryNmae",
                table: "BookCategories",
                unicode: false,
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookCategoryNmae",
                table: "BookCategories",
                newName: "BookCategoryName");

            migrationBuilder.AlterColumn<string>(
                name: "BookCategoryName",
                table: "BookCategories",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 15,
                oldNullable: true);
        }
    }
}
