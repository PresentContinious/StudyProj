using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models;

public class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? FileId { get; set; }
    public bool Certification { get; set; }
    public int? Price { get; set; }
    public int TotalReviews { get; set; }
    public int TotalMark { get; set; }
    [NotMapped] public int Rating => TotalReviews == 0 ? 0 : TotalMark / TotalReviews;

    public string TeacherId { get; set; } = null!;
    public Teacher Teacher { get; set; } = null!;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public ICollection<Session> Sessions { get; set; } = null!;
    public ICollection<Student> Students { get; set; } = null!;
    public ICollection<StudentCourse> StudentCourses { get; set; } = null!;
}
