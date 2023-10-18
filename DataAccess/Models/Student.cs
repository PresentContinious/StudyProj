namespace DataAccess.Models;

public class Student : User
{
    public ICollection<Course> Courses { get; set; } = null!;
    public ICollection<StudentCourse> StudentCourses { get; set; } = null!;
    public ICollection<CompletedLesson> CompletedLessons { get; set; } = null!;
}
