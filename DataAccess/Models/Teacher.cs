namespace DataAccess.Models;

public class Teacher : User
{
    public ICollection<Course> Courses { get; set; } = null!;
}
