using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using DataAccess.Repositories.Operations;
using Microsoft.AspNetCore.Identity;
using StudentHub.Core.Interfaces;
using StudentHub.Core.Services;

namespace StudentHub.Core.WorkModel;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context,
        UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        UserAuthentication = new UserAuthenticationRepository(userManager, roleManager);
        UserRepository = new GenericRepository<User>(_context);
        CourseRepository = new GenericRepository<Course>(_context);
        CategoryRepository = new GenericRepository<Category>(_context);
        SessionRepository = new GenericRepository<Session>(_context);
        VideoRepository = new GenericRepository<Video>(_context);
        StudentRepository = new GenericRepository<Student>(_context);
        TeacherRepository = new GenericRepository<Teacher>(_context);
        DataRepository = new GenericRepository<Data>(_context);
        StudentCourseRepository = new GenericRepository<StudentCourse>(_context);
        LessonRepository = new GenericRepository<Lesson>(_context);
    }

    public IGenericRepository<User> UserRepository { get; }
    public IGenericRepository<Course> CourseRepository { get; }
    public IGenericRepository<Lesson> LessonRepository { get; }
    public IGenericRepository<Category> CategoryRepository { get; }
    public IGenericRepository<Session> SessionRepository { get; }
    public IGenericRepository<Video> VideoRepository { get; }
    public IGenericRepository<Student> StudentRepository { get; }
    public IGenericRepository<Teacher> TeacherRepository { get; }
    public IGenericRepository<Data> DataRepository { get; }
    public IGenericRepository<StudentCourse> StudentCourseRepository { get; }
    public IUserAuthenticationRepository UserAuthentication { get; }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    private bool _disposed;

    private void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _context.Dispose();

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
