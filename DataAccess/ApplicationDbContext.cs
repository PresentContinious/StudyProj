using DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<Teacher> Teachers { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<QuizAnswers> QuizAnswers { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Student>()
            .HasMany(c => c.Courses)
            .WithMany(s => s.Students)
            .UsingEntity<StudentCourse>(
                j => j.HasOne(sc => sc.Course)
                    .WithMany(c => c.StudentCourses)
                    .HasForeignKey(sc => sc.CourseId)
                    .OnDelete(DeleteBehavior.NoAction),
                j => j.HasOne(sc => sc.Student)
                    .WithMany(s => s.StudentCourses)
                    .HasForeignKey(sc => sc.StudentId)
                    .OnDelete(DeleteBehavior.NoAction),
                j => j.HasKey(sc => new { sc.CourseId, sc.StudentId }));

        builder.Entity<Lesson>()
            .HasMany(l => l.StudentCourses)
            .WithMany(s => s.Lessons)
            .UsingEntity<CompletedLesson>(
                j => j.HasOne(sc => sc.StudentCourse)
                    .WithMany(c => c.CompletedLessons)
                    .HasForeignKey(sc => new { sc.StudentCourseCourseId, sc.StudentCourseStudentId })
                    .OnDelete(DeleteBehavior.NoAction),
                j => j.HasOne(sc => sc.Lesson)
                    .WithMany(s => s.CompletedLessons)
                    .HasForeignKey(sc => sc.LessonId)
                    .OnDelete(DeleteBehavior.NoAction),
                j => j.HasKey(sc => new { sc.LessonId, sc.StudentCourseCourseId, sc.StudentCourseStudentId }));

        builder.Entity<Category>()
            .HasData(new List<Category>
            {
                new() { Id = 1, Name = "ІТ" },
                new() { Id = 2, Name = "Аналіз даних" },
                new() { Id = 3, Name = "Бізнес" },
                new() { Id = 4, Name = "Гуманітарні науки" },
                new() { Id = 5, Name = "Графічний дизайн" },
                new() { Id = 6, Name = "Журналістика" },
                new() { Id = 7, Name = "Іноземні мови" },
                new() { Id = 8, Name = "Особистий розвиток" },
                new() { Id = 9, Name = "Охорона здоров’я" },
                new() { Id = 10, Name = "Підготовка до ЗНО" },
                new() { Id = 11, Name = "Право" },
                new() { Id = 12, Name = "Психологія" },
                new() { Id = 13, Name = "Культура" },
                new() { Id = 14, Name = "Мистецтво" },
                new() { Id = 15, Name = "Суспільні науки" }
            });

        builder.Entity<Data>()
            .HasData(new List<Data>
            {
                new() { Id = 1, Type = DataType.Skills, Name = "Soft Skills" },
                new() { Id = 2, Type = DataType.Skills, Name = "Hard Skills" },
                new() { Id = 3, Type = DataType.Skills, Name = "Іноземні мови" },
                new() { Id = 4, Type = DataType.Skills, Name = "Особистий розвиток" },
                new() { Id = 5, Type = DataType.Skills, Name = "Підготовка до ЗНО" },
                new() { Id = 6, Type = DataType.Skills, Name = "Підготовка до вступу" },
                new() { Id = 7, Type = DataType.Skills, Name = "Підготовка до іспитів" },
                new() { Id = 8, Type = DataType.Aim, Name = "Підвищення кваліфікації" },
                new() { Id = 9, Type = DataType.Aim, Name = "Підвищення рівня знань" },
                new() { Id = 10, Type = DataType.Aim, Name = "Підвищення рівня володіння іноземною мовою" },
                new() { Id = 11, Type = DataType.Occupation, Name = "Адміністрація" },
                new() { Id = 12, Type = DataType.Occupation, Name = "Бухгалтерія" },
                new() { Id = 13, Type = DataType.Occupation, Name = "Веб-розробка" },
                new() { Id = 14, Type = DataType.Occupation, Name = "Видавництво" },
                new() { Id = 15, Type = DataType.Occupation, Name = "Видавнича справа" },
                new() { Id = 16, Type = DataType.WantedLevel, Name = "Початковий" },
                new() { Id = 17, Type = DataType.WantedLevel, Name = "Середній" },
                new() { Id = 18, Type = DataType.WantedLevel, Name = "Високий" },
                new() { Id = 19, Type = DataType.WantedLevel, Name = "Дуже високий" },
            });
    }
}
