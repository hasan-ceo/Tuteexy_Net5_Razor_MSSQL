using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tuteexy.DataAccess.Migrations
{
    public partial class safsdfsakl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LmsHomework",
                columns: table => new
                {
                    HomeworkID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassRoomID = table.Column<long>(nullable: false),
                    TeacherID = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(maxLength: 64, nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(maxLength: 4096, nullable: true),
                    DateAssigned = table.Column<DateTime>(nullable: false),
                    DateDue = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsHomework", x => x.HomeworkID);
                    table.ForeignKey(
                        name: "FK_LmsHomework_LmsClassRooms_ClassRoomID",
                        column: x => x.ClassRoomID,
                        principalTable: "LmsClassRooms",
                        principalColumn: "ClassRoomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LmsHomework_AspNetUsers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LmsHomework_ClassRoomID",
                table: "LmsHomework",
                column: "ClassRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsHomework_TeacherID",
                table: "LmsHomework",
                column: "TeacherID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LmsHomework");
        }
    }
}
