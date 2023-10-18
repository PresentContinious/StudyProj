namespace DataAccess.Models;

public class Lesson
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public LessonType Type { get; set; }

    public Video? Video { get; set; }
    public ICollection<Document>? Documents { get; set; }
    public ICollection<Question>? Questions { get; set; }

    public int SessionId { get; set; }
    public Session? Session { get; set; }

    public ICollection<StudentCourse> StudentCourses { get; set; } = null!;
    public ICollection<CompletedLesson> CompletedLessons { get; set; } = null!;
}

public enum LessonType
{
    Video,
    Document,
    Quiz
}
