using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectTrainingToiecs.Migrations
{
    public partial class InitialCreateLastUpdateUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Units",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "Units",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "Units");
        }
    }
}
