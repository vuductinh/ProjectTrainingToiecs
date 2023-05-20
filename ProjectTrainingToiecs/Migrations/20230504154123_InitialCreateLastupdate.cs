using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectTrainingToiecs.Migrations
{
    public partial class InitialCreateLastupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "Course",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "Course");
        }
    }
}
