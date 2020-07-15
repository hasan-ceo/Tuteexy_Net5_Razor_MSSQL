using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tuteexy.DataAccess.Migrations
{
    public partial class safsdfsa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "LmsSchoolTeacher",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "LmsSchoolTeacher",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "LmsSchoolTeacher",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "LmsClassRoomStudent",
                columns: table => new
                {
                    ClassRoomStudentID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassRoomID = table.Column<long>(nullable: false),
                    StudentID = table.Column<string>(nullable: true),
                    ApprovedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ApprovedDate = table.Column<DateTime>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsClassRoomStudent", x => x.ClassRoomStudentID);
                    table.ForeignKey(
                        name: "FK_LmsClassRoomStudent_LmsClassRooms_ClassRoomID",
                        column: x => x.ClassRoomID,
                        principalTable: "LmsClassRooms",
                        principalColumn: "ClassRoomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LmsClassRoomStudent_AspNetUsers_StudentID",
                        column: x => x.StudentID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LmsClassRoomStudent_ClassRoomID",
                table: "LmsClassRoomStudent",
                column: "ClassRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsClassRoomStudent_StudentID",
                table: "LmsClassRoomStudent",
                column: "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LmsClassRoomStudent");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "LmsSchoolTeacher");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "LmsSchoolTeacher");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "LmsSchoolTeacher");
        }
    }
}
