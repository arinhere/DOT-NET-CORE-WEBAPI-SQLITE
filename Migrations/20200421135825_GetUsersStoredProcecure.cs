using Microsoft.EntityFrameworkCore.Migrations;

namespace DOT_NET_CORE_WEBAPI_SQLITE.Migrations
{
    public partial class GetUsersStoredProcecure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"Create procedure sp_getUsers
                        as
                        Begin
                            select * from Users
                        End";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"drop procedure sp_getUsers";
            migrationBuilder.Sql(sp);
        }
    }
}
