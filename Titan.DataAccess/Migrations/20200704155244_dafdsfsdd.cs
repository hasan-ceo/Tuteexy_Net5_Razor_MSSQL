using Microsoft.EntityFrameworkCore.Migrations;

namespace Titan.DataAccess.Migrations
{
    public partial class dafdsfsdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "eclasses",
                columns: table => new
                {
                    eclassID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    classname = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eclasses", x => x.eclassID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eclasses");
        }
    }
}
