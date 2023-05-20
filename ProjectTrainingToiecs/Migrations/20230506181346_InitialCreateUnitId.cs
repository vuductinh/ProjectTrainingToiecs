using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectTrainingToiecs.Migrations
{
    public partial class InitialCreateUnitId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "TestDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "TestDetails");
        }
    }
}
