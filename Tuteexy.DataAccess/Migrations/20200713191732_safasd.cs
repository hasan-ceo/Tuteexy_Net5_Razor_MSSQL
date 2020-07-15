using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tuteexy.DataAccess.Migrations
{
    public partial class safasd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LmsSchoolTeacher",
                columns: table => new
                {
                    SchoolTeacherID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolID = table.Column<long>(nullable: false),
                    TeacherID = table.Column<string>(nullable: true),
                    AuthorizedBy = table.Column<string>(maxLength: 50, nullable: true),
                    AuthorizedDate = table.Column<DateTime>(nullable: false),
                    IsAuthorizedTeacher = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsSchoolTeacher", x => x.SchoolTeacherID);
                    table.ForeignKey(
                        name: "FK_LmsSchoolTeacher_LmsSchools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "LmsSchools",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LmsSchoolTeacher_AspNetUsers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LmsSchoolTeacher_SchoolID",
                table: "LmsSchoolTeacher",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsSchoolTeacher_TeacherID",
                table: "LmsSchoolTeacher",
                column: "TeacherID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LmsSchoolTeacher");
        }
    }
}
