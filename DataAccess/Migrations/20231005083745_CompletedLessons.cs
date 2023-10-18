using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CompletedLessons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonStudentCourse");

            migrationBuilder.CreateTable(
                name: "CompletedLesson",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    StudentCourseStudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentCourseCourseId = table.Column<int>(type: "int", nullable: false),
                    TestPercentage = table.Column<int>(type: "int", nullable: true),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedLesson", x => new { x.LessonId, x.StudentCourseCourseId, x.StudentCourseStudentId });
                    table.ForeignKey(
                        name: "FK_CompletedLesson_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompletedLesson_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompletedLesson_StudentCourse_StudentCourseCourseId_StudentCourseStudentId",
                        columns: x => new { x.StudentCourseCourseId, x.StudentCourseStudentId },
                        principalTable: "StudentCourse",
                        principalColumns: new[] { "CourseId", "StudentId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedLesson_StudentCourseCourseId_StudentCourseStudentId",
                table: "CompletedLesson",
                columns: new[] { "StudentCourseCourseId", "StudentCourseStudentId" });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedLesson_StudentId",
                table: "CompletedLesson",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedLesson");

            migrationBuilder.CreateTable(
                name: "LessonStudentCourse",
                columns: table => new
                {
                    CompletedLessonsId = table.Column<int>(type: "int", nullable: false),
                    StudentCoursesCourseId = table.Column<int>(type: "int", nullable: false),
                    StudentCoursesStudentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonStudentCourse", x => new { x.CompletedLessonsId, x.StudentCoursesCourseId, x.StudentCoursesStudentId });
                    table.ForeignKey(
                        name: "FK_LessonStudentCourse_Lesson_CompletedLessonsId",
                        column: x => x.CompletedLessonsId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonStudentCourse_StudentCourse_StudentCoursesCourseId_StudentCoursesStudentId",
                        columns: x => new { x.StudentCoursesCourseId, x.StudentCoursesStudentId },
                        principalTable: "StudentCourse",
                        principalColumns: new[] { "CourseId", "StudentId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonStudentCourse_StudentCoursesCourseId_StudentCoursesStudentId",
                table: "LessonStudentCourse",
                columns: new[] { "StudentCoursesCourseId", "StudentCoursesStudentId" });
        }
    }
}
