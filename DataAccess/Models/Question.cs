namespace DataAccess.Models;

public class Question
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public int LessonId { get; set; }
    public Lesson? Lesson { get; set; }

    public ICollection<Answer> Answers { get; set; } = null!;
}
