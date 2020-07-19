using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tuteexy.DataAccess.Migrations
{
    public partial class jjjjjjj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LmsClassRoutine",
                columns: table => new
                {
                    ClassRoutineID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    ClassRoomID = table.Column<long>(nullable: false),
                    DayName = table.Column<string>(maxLength: 50, nullable: false),
                    Period1 = table.Column<string>(maxLength: 50, nullable: false),
                    Period2 = table.Column<string>(maxLength: 50, nullable: false),
                    Period3 = table.Column<string>(maxLength: 50, nullable: false),
                    Period4 = table.Column<string>(maxLength: 50, nullable: false),
                    Period5 = table.Column<string>(maxLength: 50, nullable: false),
                    Period6 = table.Column<string>(maxLength: 50, nullable: false),
                    Period7 = table.Column<string>(maxLength: 50, nullable: false),
                    Period8 = table.Column<string>(maxLength: 50, nullable: false),
                    Period9 = table.Column<string>(maxLength: 50, nullable: false),
                    Period10 = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsClassRoutine", x => x.ClassRoutineID);
                    table.ForeignKey(
                        name: "FK_LmsClassRoutine_LmsClassRooms_ClassRoomID",
                        column: x => x.ClassRoomID,
                        principalTable: "LmsClassRooms",
                        principalColumn: "ClassRoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LmsClassRoutine_ClassRoomID",
                table: "LmsClassRoutine",
                column: "ClassRoomID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LmsClassRoutine");
        }
    }
}
