using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tuteexy.DataAccess.Migrations
{
    public partial class safsdfsaklj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LmsSubject",
                columns: table => new
                {
                    SubjectID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    SchoolID = table.Column<long>(nullable: false),
                    SubjectName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsSubject", x => x.SubjectID);
                    table.ForeignKey(
                        name: "FK_LmsSubject_LmsSchools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "LmsSchools",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LmsSubject_SchoolID",
                table: "LmsSubject",
                column: "SchoolID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LmsSubject");
        }
    }
}
