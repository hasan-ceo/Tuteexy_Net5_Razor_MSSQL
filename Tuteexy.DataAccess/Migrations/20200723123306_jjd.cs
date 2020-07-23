using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tuteexy.DataAccess.Migrations
{
    public partial class jjd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LmsClassRoomNotice",
                columns: table => new
                {
                    ClassRoomNoticeID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ClassRoomID = table.Column<long>(nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: false),
                    ScheduleDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsClassRoomNotice", x => x.ClassRoomNoticeID);
                    table.ForeignKey(
                        name: "FK_LmsClassRoomNotice_LmsClassRoom_ClassRoomID",
                        column: x => x.ClassRoomID,
                        principalTable: "LmsClassRoom",
                        principalColumn: "ClassRoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LmsClassRoomNotice_ClassRoomID",
                table: "LmsClassRoomNotice",
                column: "ClassRoomID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LmsClassRoomNotice");
        }
    }
}
