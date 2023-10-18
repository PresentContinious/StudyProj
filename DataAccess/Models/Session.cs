namespace DataAccess.Models;

public class Session
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CourseId { get; set; }
    public Course? Course { get; set; }

    public ICollection<Lesson> Lessons { get; set; } = null!;
}
