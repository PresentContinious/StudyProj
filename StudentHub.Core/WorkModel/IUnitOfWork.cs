using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using StudentHub.Core.Interfaces;

namespace StudentHub.Core.WorkModel;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<User> UserRepository { get; }
    IGenericRepository<Course> CourseRepository { get; }
    IGenericRepository<Lesson> LessonRepository { get; }
    IGenericRepository<Category> CategoryRepository { get; }
    IGenericRepository<Session> SessionRepository { get; }
    IGenericRepository<Video> VideoRepository { get; }
    IGenericRepository<Student> StudentRepository { get; }
    IGenericRepository<Teacher> TeacherRepository { get; }
    IGenericRepository<Data> DataRepository { get; }
    IGenericRepository<StudentCourse> StudentCourseRepository { get; }
    IUserAuthenticationRepository UserAuthentication { get; }
    Task SaveAsync();
}
