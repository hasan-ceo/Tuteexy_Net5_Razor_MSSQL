using Microsoft.EntityFrameworkCore.Migrations;

namespace Titan.DataAccess.Migrations
{
    public partial class dafdsfsddd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "year",
                table: "eclasses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "year",
                table: "eclasses");
        }
    }
}
