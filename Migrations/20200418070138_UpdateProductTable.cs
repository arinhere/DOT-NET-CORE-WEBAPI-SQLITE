using Microsoft.EntityFrameworkCore.Migrations;

namespace DOT_NET_CORE_WEBAPI_SQLITE.Migrations
{
    public partial class UpdateProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CloudinaryId",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloudinaryId",
                table: "Products");
        }
    }
}
