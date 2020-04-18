using Microsoft.EntityFrameworkCore.Migrations;

namespace DOT_NET_CORE_WEBAPI_SQLITE.Migrations
{
    public partial class UpdateProuctTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CloudinaryUrl",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloudinaryUrl",
                table: "Products");
        }
    }
}
