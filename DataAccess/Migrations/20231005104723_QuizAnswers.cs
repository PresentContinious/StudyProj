using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class QuizAnswers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuizAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: false),
                    CompletedLessonLessonId = table.Column<int>(type: "int", nullable: true),
                    CompletedLessonStudentCourseCourseId = table.Column<int>(type: "int", nullable: true),
                    CompletedLessonStudentCourseStudentId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizAnswers_CompletedLesson_CompletedLessonLessonId_CompletedLessonStudentCourseCourseId_CompletedLessonStudentCourseStudent~",
                        columns: x => new { x.CompletedLessonLessonId, x.CompletedLessonStudentCourseCourseId, x.CompletedLessonStudentCourseStudentId },
                        principalTable: "CompletedLesson",
                        principalColumns: new[] { "LessonId", "StudentCourseCourseId", "StudentCourseStudentId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizAnswers_CompletedLessonLessonId_CompletedLessonStudentCourseCourseId_CompletedLessonStudentCourseStudentId",
                table: "QuizAnswers",
                columns: new[] { "CompletedLessonLessonId", "CompletedLessonStudentCourseCourseId", "CompletedLessonStudentCourseStudentId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizAnswers");
        }
    }
}
