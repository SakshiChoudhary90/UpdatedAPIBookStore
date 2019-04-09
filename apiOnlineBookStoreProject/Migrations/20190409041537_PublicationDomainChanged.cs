using Microsoft.EntityFrameworkCore.Migrations;

namespace apiOnlineBookStoreProject.Migrations
{
    public partial class PublicationDomainChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PublicationName",
                table: "Publications",
                unicode: false,
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PublicationName",
                table: "Publications",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 5,
                oldNullable: true);
        }
    }
}
