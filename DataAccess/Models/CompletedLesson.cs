namespace DataAccess.Models;

public class CompletedLesson
{
    public int LessonId { get; set; }
    public Lesson? Lesson { get; set; }

    public string StudentCourseStudentId { get; set; } = null!;
    public int StudentCourseCourseId { get; set; }
    public StudentCourse? StudentCourse { get; set; }

    public int? TestPercentage { get; set; }

    public ICollection<QuizAnswers> QuizAnswers { get; set; } = null!;
}

public class QuizAnswers
{
    public int Id { get; set; }

    public int QuestionId { get; set; }
    public int AnswerId { get; set; }

    public CompletedLesson? CompletedLesson { get; set; }
}
