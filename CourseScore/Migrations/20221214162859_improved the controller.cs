using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseScore.Migrations
{
    public partial class improvedthecontroller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseCodes",
                columns: table => new
                {
                    matricNo = table.Column<string>(nullable: false),
                    courseCode = table.Column<string>(nullable: true),
                    score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCodes", x => x.matricNo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCodes");
        }
    }
}
