using Microsoft.EntityFrameworkCore.Migrations;

namespace Titan.DataAccess.Migrations
{
    public partial class randcompd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "randomComp",
                columns: table => new
                {
                    randomCompId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic = table.Column<string>(maxLength: 50, nullable: false),
                    advantage = table.Column<string>(nullable: true),
                    disadvantage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_randomComp", x => x.randomCompId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "randomComp");

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
    }
}
