using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tuteexy.DataAccess.Migrations
{
    public partial class asdfasdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodTime1",
                table: "LmsClassRoutine",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodTime10",
                table: "LmsClassRoutine",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodTime2",
                table: "LmsClassRoutine",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodTime3",
                table: "LmsClassRoutine",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodTime4",
                table: "LmsClassRoutine",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodTime5",
                table: "LmsClassRoutine",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodTime6",
                table: "LmsClassRoutine",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodTime7",
                table: "LmsClassRoutine",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodTime8",
                table: "LmsClassRoutine",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodTime9",
                table: "LmsClassRoutine",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeriodTime1",
                table: "LmsClassRoutine");

            migrationBuilder.DropColumn(
                name: "PeriodTime10",
                table: "LmsClassRoutine");

            migrationBuilder.DropColumn(
                name: "PeriodTime2",
                table: "LmsClassRoutine");

            migrationBuilder.DropColumn(
                name: "PeriodTime3",
                table: "LmsClassRoutine");

            migrationBuilder.DropColumn(
                name: "PeriodTime4",
                table: "LmsClassRoutine");

            migrationBuilder.DropColumn(
                name: "PeriodTime5",
                table: "LmsClassRoutine");

            migrationBuilder.DropColumn(
                name: "PeriodTime6",
                table: "LmsClassRoutine");

            migrationBuilder.DropColumn(
                name: "PeriodTime7",
                table: "LmsClassRoutine");

            migrationBuilder.DropColumn(
                name: "PeriodTime8",
                table: "LmsClassRoutine");

            migrationBuilder.DropColumn(
                name: "PeriodTime9",
                table: "LmsClassRoutine");
        }
    }
}
