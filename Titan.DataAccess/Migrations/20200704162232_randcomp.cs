using Microsoft.EntityFrameworkCore.Migrations;

namespace Titan.DataAccess.Migrations
{
    public partial class randcomp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_eclasses",
                table: "eclasses");

            migrationBuilder.RenameTable(
                name: "eclasses",
                newName: "eclass");

            migrationBuilder.AddPrimaryKey(
                name: "PK_eclass",
                table: "eclass",
                column: "eclassID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_eclass",
                table: "eclass");

            migrationBuilder.RenameTable(
                name: "eclass",
                newName: "eclasses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_eclasses",
                table: "eclasses",
                column: "eclassID");
        }
    }
}
