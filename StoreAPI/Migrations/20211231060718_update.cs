using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAPI.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                schema: "Str",
                table: "StoreSchedules");

            migrationBuilder.RenameColumn(
                name: "Endtime",
                schema: "Str",
                table: "StoreSchedules",
                newName: "EndTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EndTime",
                schema: "Str",
                table: "StoreSchedules",
                newName: "Endtime");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                schema: "Str",
                table: "StoreSchedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
