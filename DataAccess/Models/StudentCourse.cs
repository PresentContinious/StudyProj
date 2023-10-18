using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models;

public class StudentCourse
{
    public string StudentId { get; set; } = null!;
    public Student? Student { get; set; }

    public int CourseId { get; set; }
    public Course? Course { get; set; }

    public bool Paid { get; set; }
    public int Progress { get; set; }

    public int CurrentLessonId { get; set; }
    public int CurrentSessionId { get; set; }

    public int Stars { get; set; }
    public string? Review { get; set; }

    [NotMapped] public bool Completed => Progress == 100;

    public int? PaymentMethodId { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }

    public ICollection<Lesson> Lessons { get; set; } = null!;
    public ICollection<CompletedLesson> CompletedLessons { get; set; } = null!;
}
