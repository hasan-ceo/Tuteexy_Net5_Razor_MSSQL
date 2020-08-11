using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tuteexy.DataAccess.Migrations
{
    public partial class hhhkls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LmsClassworkSheet",
                columns: table => new
                {
                    ClassworkSheetID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassworkID = table.Column<long>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 1024, nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsClassworkSheet", x => x.ClassworkSheetID);
                    table.ForeignKey(
                        name: "FK_LmsClassworkSheet_LmsClasswork_ClassworkID",
                        column: x => x.ClassworkID,
                        principalTable: "LmsClasswork",
                        principalColumn: "ClassworkID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LmsClassworkSheet_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LmsClassworkSheet_ClassworkID",
                table: "LmsClassworkSheet",
                column: "ClassworkID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsClassworkSheet_UserID",
                table: "LmsClassworkSheet",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LmsClassworkSheet");
        }
    }
}
