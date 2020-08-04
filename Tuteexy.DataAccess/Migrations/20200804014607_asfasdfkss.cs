using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tuteexy.DataAccess.Migrations
{
    public partial class asfasdfkss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LmsClasswork",
                columns: table => new
                {
                    ClassworkID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassRoomID = table.Column<long>(nullable: false),
                    TeacherID = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(maxLength: 64, nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    DateAssigned = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStart = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeEnd = table.Column<DateTime>(type: "datetime", nullable: false),
                    RefLink1 = table.Column<string>(maxLength: 150, nullable: true),
                    RefLink2 = table.Column<string>(maxLength: 150, nullable: true),
                    RefLink3 = table.Column<string>(maxLength: 150, nullable: true),
                    RefLink4 = table.Column<string>(maxLength: 150, nullable: true),
                    RefLink5 = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsClasswork", x => x.ClassworkID);
                    table.ForeignKey(
                        name: "FK_LmsClasswork_LmsClassRoom_ClassRoomID",
                        column: x => x.ClassRoomID,
                        principalTable: "LmsClassRoom",
                        principalColumn: "ClassRoomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LmsClasswork_AspNetUsers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LmsClasswork_ClassRoomID",
                table: "LmsClasswork",
                column: "ClassRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsClasswork_TeacherID",
                table: "LmsClasswork",
                column: "TeacherID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LmsClasswork");
        }
    }
}
