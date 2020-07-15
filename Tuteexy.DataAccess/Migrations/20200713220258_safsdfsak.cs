using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tuteexy.DataAccess.Migrations
{
    public partial class safsdfsak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorizedBy",
                table: "LmsSchoolTeacher");

            migrationBuilder.DropColumn(
                name: "AuthorizedDate",
                table: "LmsSchoolTeacher");

            migrationBuilder.DropColumn(
                name: "IsAuthorizedTeacher",
                table: "LmsSchoolTeacher");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorizedBy",
                table: "LmsSchoolTeacher",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AuthorizedDate",
                table: "LmsSchoolTeacher",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsAuthorizedTeacher",
                table: "LmsSchoolTeacher",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
