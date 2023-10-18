using DataAccess.Models;
using StudentHub.Core.DTOs;
using StudentHub.Core.WorkModel;

namespace StudentHub.Middlewares;

/// <summary>
/// Middleware for initial data filling
/// </summary>
public class InitialMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="next"></param>
    public InitialMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invokes middleware
    /// </summary>
    /// <param name="context"></param>
    /// <param name="workModel"></param>
    public async Task InvokeAsync(HttpContext context, IUnitOfWork workModel)
    {
        if (context.Request.Path.Value == "/middleware/initial")
        {
            if (await workModel.TeacherRepository.GetFirstAsync() is not null)
            {
                await context.Response.WriteAsync("Database is not empty");
                return;
            }

            var teacherDto = new UserRegistrationDto()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "teacher@email.com",
                PhoneNumber = "0991234567",
                Teacher = true,
                Password = "teacherPassword"
            };

            var studentDto = new UserRegistrationDto()
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "student@email.com",
                PhoneNumber = "0631234567",
                Teacher = false,
                Password = "studentPassword"
            };

            await workModel.UserAuthentication.RegisterStudentAsync(studentDto);
            await workModel.UserAuthentication.RegisterTeacherAsync(teacherDto);

            var teacher = (await workModel.UserRepository
                .GetFirstAsync(t => t.Email == teacherDto.Email, "Courses"))!;

            var course = new Course
            {
                Name = "Math",
                Description = "Math course",
                Certification = true,
                Price = 100,
                TeacherId = teacher.Id,
                CategoryId = 2,
                FileId = "170POkI9tURzjY5TdAF2L6G1je5SspAd6",
                Sessions = new List<Session>
                {
                    new()
                    {
                        Name = "Introduction",
                        Lessons = new List<Lesson>
                        {
                            new()
                            {
                                Name = "Introduction to Math",
                                Type = LessonType.Video,
                                Video = new Video
                                {
                                    FileId = "1wR5IFcm3liHl_r-IACIkVm7BRS2HSfwQ",
                                    DurationInMinutes = 1,
                                    Name = "Devs watching"
                                }
                            },
                            new()
                            {
                                Name = "Second Step",
                                Type = LessonType.Quiz,
                                Questions = new List<Question>
                                {
                                    new()
                                    {
                                        Text = "What is 2 + 2?",
                                        Answers = new List<Answer>
                                        {
                                            new() { Text = "1", IsCorrect = false },
                                            new() { Text = "2", IsCorrect = false },
                                            new() { Text = "3", IsCorrect = false },
                                            new() { Text = "4", IsCorrect = true }
                                        }
                                    },
                                    new()
                                    {
                                        Text = "What is 2 * 2?",
                                        Answers = new List<Answer>
                                        {
                                            new() { Text = "1", IsCorrect = false },
                                            new() { Text = "2", IsCorrect = false },
                                            new() { Text = "3", IsCorrect = false },
                                            new() { Text = "4", IsCorrect = true }
                                        }
                                    },
                                    new()
                                    {
                                        Text = "What is 2 / 2?",
                                        Answers = new List<Answer>
                                        {
                                            new() { Text = "1", IsCorrect = true },
                                            new() { Text = "2", IsCorrect = false },
                                            new() { Text = "3", IsCorrect = false },
                                            new() { Text = "4", IsCorrect = false }
                                        }
                                    },
                                    new()
                                    {
                                        Text = "What is 2 - 2?",
                                        Answers = new List<Answer>
                                        {
                                            new() { Text = "1", IsCorrect = false },
                                            new() { Text = "2", IsCorrect = false },
                                            new() { Text = "3", IsCorrect = false },
                                            new() { Text = "0", IsCorrect = true }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    new()
                    {
                        Name = "Advanced",
                        Lessons = new List<Lesson>
                        {
                            new()
                            {
                                Name = "Advanced Math",
                                Type = LessonType.Video,
                                Video = new Video
                                {
                                    FileId = "1T6B0U-xJ8VrOcsp15T7J3Z4mdP4xZj8G",
                                    DurationInMinutes = 1,
                                    Name = "Golf"
                                }
                            },
                            new()
                            {
                                Name = "Finish",
                                Type = LessonType.Document,
                                Documents = new List<Document>
                                {
                                    new() { Name = "First Doc", FileId = "1hjn-nWfgYrcd6QnO7cyVfT0v3_HUBN0c" },
                                    new() { Name = "Second Doc", FileId = "106sb3gZBX-NCPb5zPr1VUQVCUHgmZHOy" },
                                    new() { Name = "Third Doc", FileId = "1Juqzrh8rSZCLSaX_bjeG7tjIMzU1XiwV" },
                                }
                            }
                        }
                    }
                }
            };

            var itCourse = new Course
            {
                Name = "IT Course",
                Description = "This course will teach you how to code and become a programmer in no time!",
                Certification = false,
                Price = 400,
                TeacherId = teacher.Id,
                CategoryId = 1,
                FileId = "142J_AOh_0ZDiTlCCIZbZicCf0_-O0NEh",
                Sessions = new List<Session>
                {
                    new()
                    {
                        Name = "Introduction",
                        Lessons = new List<Lesson>
                        {
                            new()
                            {
                                Type = LessonType.Video,
                                Name = "Introduction to IT",
                                Video = new Video
                                {
                                    Name = "Intoduction Video",
                                    FileId = "1qKjX6auX9lVT80Lwyz-r1LtqCvkTmxHr",
                                    DurationInMinutes = 7
                                }
                            },
                            new()
                            {
                                Name = "Final Test",
                                Type = LessonType.Quiz,
                                Questions = new List<Question>
                                {
                                    new()
                                    {
                                        Text = "What is IT?",
                                        Answers = new List<Answer>
                                        {
                                            new() { Text = "Information Technology", IsCorrect = true },
                                            new() { Text = "Information Theory", IsCorrect = false },
                                            new() { Text = "Information Transfer", IsCorrect = false },
                                            new() { Text = "Information Transfer", IsCorrect = false }
                                        }
                                    },
                                    new()
                                    {
                                        Text = "What is the difference between IT and ICT?",
                                        Answers = new List<Answer>
                                        {
                                            new() { Text = "IT is a subset of ICT", IsCorrect = true },
                                            new() { Text = "ICT is a subset of IT", IsCorrect = false },
                                            new() { Text = "They are the same", IsCorrect = false },
                                            new() { Text = "There is no difference", IsCorrect = false }
                                        }
                                    },
                                    new()
                                    {
                                        Text = "What is the difference between IT and Computer Science?",
                                        Answers = new List<Answer>
                                        {
                                            new() { Text = "IT is a subset of Computer Science", IsCorrect = false },
                                            new() { Text = "Computer Science is a subset of IT", IsCorrect = false },
                                            new() { Text = "They are the same", IsCorrect = false },
                                            new() { Text = "There is no difference", IsCorrect = true }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var businessCourse = new Course
            {
                Name = "Business Course",
                Description = "This course will teach you how to do business and become a businessman in no time!",
                Certification = false,
                Price = null,
                TeacherId = teacher.Id,
                CategoryId = 3,
                FileId = "1-wVdq_mSVpMLAcadK_YJ8MZ0qTuBw-SZ",
                Sessions = new List<Session>
                {
                    new()
                    {
                        Name = "Initial Lesson", Lessons = new List<Lesson>
                        {
                            new()
                            {
                                Name = "Introduction to Business",
                                Type = LessonType.Document,
                                Documents = new List<Document>
                                {
                                    new() { Name = "First Doc", FileId = "1hjn-nWfgYrcd6QnO7cyVfT0v3_HUBN0c" },
                                    new() { Name = "Second Doc", FileId = "106sb3gZBX-NCPb5zPr1VUQVCUHgmZHOy" },
                                    new() { Name = "Third Doc", FileId = "1Juqzrh8rSZCLSaX_bjeG7tjIMzU1XiwV" },
                                }
                            }
                        }
                    },
                    new()
                    {
                        Name = "Final Test", Lessons = new List<Lesson>
                        {
                            new()
                            {
                                Name = "Final Test",
                                Type = LessonType.Quiz,
                                Questions = new List<Question>
                                {
                                    new()
                                    {
                                        Text = "What is Business?",
                                        Answers = new List<Answer>
                                        {
                                            new()
                                            {
                                                Text =
                                                    "Business is the activity of making one's living or making money by producing or buying and selling products (such as goods and services).",
                                                IsCorrect = true
                                            },
                                            new()
                                            {
                                                Text =
                                                    "Business is the activity of making one's living or making money by producing or buying and selling products (such as goods and services).",
                                                IsCorrect = true
                                            },
                                            new()
                                            {
                                                Text =
                                                    "Business is the activity of making one's living or making money by producing or buying and selling products (such as goods and services).",
                                                IsCorrect = true
                                            },
                                            new()
                                            {
                                                Text =
                                                    "Business is the activity of making one's living or making money by producing or buying and selling products (such as goods and services).",
                                                IsCorrect = true
                                            }
                                        }
                                    },
                                    new()
                                    {
                                        Text = "What is the difference between Business and Entrepreneurship?",
                                        Answers = new List<Answer>
                                        {
                                            new()
                                            {
                                                Text = "Business is a subset of Entrepreneurship", IsCorrect = false
                                            },
                                            new()
                                            {
                                                Text = "Entrepreneurship is a subset of Business", IsCorrect = false
                                            },
                                            new() { Text = "They are the same", IsCorrect = false },
                                            new() { Text = "There is no difference", IsCorrect = true }
                                        }
                                    },
                                    new()
                                    {
                                        Text = "What is the difference between Business and Entrepreneurship?",
                                        Answers = new List<Answer>
                                        {
                                            new()
                                            {
                                                Text = "Business is a subset of Entrepreneurship", IsCorrect = false
                                            },
                                            new()
                                            {
                                                Text = "Entrepreneurship is a subset of Business", IsCorrect = false
                                            },
                                            new() { Text = "They are the same", IsCorrect = false },
                                            new() { Text = "There is no difference", IsCorrect = true }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            await workModel.CourseRepository.InsertAsync(course);
            await workModel.CourseRepository.InsertAsync(itCourse);
            await workModel.CourseRepository.InsertAsync(businessCourse);

            await workModel.SaveAsync();

            await context.Response.WriteAsJsonAsync(new
            {
                teacherCredentials = teacherDto,
                studentCredentials = studentDto,
                message = "Database is filled with initial data"
            });
        }
        else
        {
            await _next(context);
        }
    }
}
