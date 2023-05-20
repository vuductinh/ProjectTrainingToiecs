using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectTrainingToiecs.Migrations
{
    public partial class InitialCreateQSDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QSA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QSB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QSC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QSD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdQS = table.Column<int>(type: "int", nullable: false),
                    RecordStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
