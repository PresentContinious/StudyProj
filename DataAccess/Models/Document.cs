namespace DataAccess.Models;

public class Document
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public string FileId { get; set; } = null!;

    public int LessonId { get; set; }
    public Lesson? Lesson { get; set; }
}
