using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectTrainingToiecs.Migrations
{
    public partial class InitialCreateScript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Scription",
                table: "TestDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Scription",
                table: "TestDetails");
        }
    }
}
