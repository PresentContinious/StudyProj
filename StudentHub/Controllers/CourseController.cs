using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Core.DTOs;
using StudentHub.Core.Interfaces;
using StudentHub.Core.WorkModel;

namespace StudentHub.Controllers;

/// <summary>
/// Course controller
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class CourseController : Controller
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="workModel"></param>
    /// <param name="driveService"></param>
    public CourseController(IUnitOfWork workModel, IDriveService driveService)
    {
        WorkModel = workModel;
        DriveService = driveService;
    }

    private IUnitOfWork WorkModel { get; }
    private IDriveService DriveService { get; }

    /// <summary>
    /// Get all courses
    /// </summary>
    /// <response code="200">Returns all courses</response>
    [HttpGet]
    [ActionName("all")]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await WorkModel.CourseRepository.GetAsync();
        return Ok(courses);
    }

    /// <summary>
    /// Get courses popular and new
    /// </summary>
    /// <response code="200">Returns courses popular and new</response>
    [HttpGet]
    [ActionName("statistics")]
    public async Task<IActionResult> GetStatisticCourses()
    {
        var courses = await WorkModel.CourseRepository
            .GetAsync(null, null, "Students", "Teacher");

        var newCourses = courses
            .OrderByDescending(c => c.Id)
            .Take(4);

        var popularCourses = courses
            .OrderByDescending(c => c.Students.Count)
            .Take(4);

        return Ok(new { newCourses, popularCourses });
    }

    /// <summary>
    /// Get teacher courses
    /// </summary>
    /// <param name="teacherId"></param>
    /// <param name="courseId"></param>
    /// <response code="200">Returns teacher courses</response>
    [HttpGet("{teacherId}/{courseId:int}")]
    [ActionName("get-teacher-courses")]
    public async Task<IActionResult> GetTeacherCourses([FromRoute] string teacherId, [FromRoute] int courseId)
    {
        var courses = await WorkModel.CourseRepository
            .GetAsync(c => c.TeacherId == teacherId && c.Id != courseId);

        var teacher = await WorkModel.UserRepository
            .GetFirstAsync(u => u.Id == teacherId);

        return Ok(new { courses, teacher });
    }

    /// <summary>
    /// Get course
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200">Returns course</response>
    [HttpGet("{id:int}")]
    [ActionName("get")]
    public async Task<IActionResult> GetCourse([FromRoute] int id)
    {
        var course = await WorkModel.CourseRepository
            .GetFirstAsync(c => c.Id == id,
                "Sessions.Lessons.Video",
                "Sessions.Lessons.Documents",
                "Sessions.Lessons.Questions.Answers",
                "Teacher",
                "StudentCourses.CompletedLessons.QuizAnswers");

        var student = await WorkModel.UserRepository
            .GetFirstAsync(u => u.Email == User.Identity!.Name);

        if (student is null)
            return Ok(new { navigate = "/sign-in" });

        if (course is null || course.TeacherId == student.Id)
            return Ok(course);

        var enrollment = course.StudentCourses
            .First(s => s.StudentId == student.Id && s.CourseId == course.Id);

        var lesson = course.Sessions
            .FirstOrDefault(s => s.Id == enrollment.CurrentSessionId)?
            .Lessons
            .FirstOrDefault(l => l.Id == enrollment.CurrentLessonId);

        var paid = course.StudentCourses
            .FirstOrDefault(s => s.StudentId == student.Id && s.CourseId == course.Id)?.Paid;

        if (paid is null || !paid.Value)
            return Ok(new { navigate = $"/view-course/{id}" });

        return Ok(new { course, enrollment, lesson });
    }

    /// <summary>
    /// Get course for view
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200">Returns course for view</response>
    [HttpGet("{id:int}")]
    [ActionName("get-view")]
    public async Task<IActionResult> GetViewCourse([FromRoute] int id)
    {
        var course = await WorkModel.CourseRepository
            .GetFirstAsync(c => c.Id == id, "Teacher", "StudentCourses", "Sessions.Lessons");

        var student = await WorkModel.UserRepository
            .GetFirstAsync(u => u.Email == User.Identity!.Name);

        var studentCourse = course?.StudentCourses
            .FirstOrDefault(s => s.StudentId == student?.Id && s.CourseId == course.Id);

        return Ok(new { course, studentCourse });
    }

    /// <summary>
    /// Update video duration
    /// </summary>
    /// <param name="videoId"></param>
    /// <response code="200">Returns updated video</response>
    /// <response code="404">If lesson not found</response>
    [HttpPost]
    [ActionName("update-video")]
    public async Task<IActionResult> UpdateVideo([FromBody] int videoId)
    {
        var video = await WorkModel.VideoRepository
            .GetFirstAsync(l => l.Id == videoId);
        if (video is null)
            return NotFound("Lesson not found");

        var duration = await DriveService.GetVideoDuration(video.FileId);
        video.DurationInMinutes = duration;

        await WorkModel.VideoRepository.UpdateAsync(video);
        await WorkModel.SaveAsync();

        return Ok(video);
    }

    /// <summary>
    /// Get course for edit
    /// </summary>
    /// <param name="courseDto"></param>
    /// <response code="200">Returns course for edit</response>
    /// <response code="404">If course not found</response>
    [HttpPost]
    [Authorize(Roles = "Teacher")]
    [ActionName("add")]
    public async Task<IActionResult> CreateCourse([FromForm] CourseDto courseDto)
    {
        var teacher = await WorkModel.UserRepository
            .GetFirstAsync(u => u.Email == User.Identity!.Name);
        if (teacher is null || teacher.Role != UserRole.Teacher)
            return NotFound(new { message = "Teacher not found" });

        var course = new Course
        {
            Name = courseDto.Name,
            Description = courseDto.Description,
            Price = courseDto.Price,
            CategoryId = courseDto.CategoryId,
            TeacherId = teacher.Id
        };

        if (courseDto.Image is not null)
        {
            var file = await DriveService.UploadFile(courseDto.Image);
            course.FileId = file.id;
        }

        await WorkModel.CourseRepository.InsertAsync(course);
        await WorkModel.SaveAsync();

        return Ok(course);
    }

    /// <summary>
    /// Update course, adds session
    /// </summary>
    /// <param name="courseId"></param>
    /// <param name="sessionDto"></param>
    /// <response code="200">Returns updated session</response>
    /// <response code="404">If course not found</response>
    [HttpPost("{courseId:int}")]
    [ActionName("add-session")]
    public async Task<IActionResult> AddSession([FromRoute] int courseId, [FromBody] SessionDto sessionDto)
    {
        var course = await WorkModel.CourseRepository
            .GetFirstAsync(c => c.Id == courseId, "Sessions");
        if (course is null)
            return NotFound(new { message = "Course not found" });

        var session = new Session
        {
            Name = sessionDto.Name
        };

        course.Sessions.Add(session);

        await WorkModel.CourseRepository.UpdateAsync(course);
        await WorkModel.SaveAsync();

        return Ok(session);
    }

    /// <summary>
    /// Update course, adds lesson
    /// </summary>
    /// <param name="sessionId"></param>
    /// <param name="lessonDto"></param>
    /// <response code="200">Returns updated lesson</response>
    /// <response code="404">If session not found</response>
    [HttpPost("{sessionId:int}")]
    [ActionName("add-lesson")]
    public async Task<IActionResult> AddLesson([FromRoute] int sessionId, [FromForm] LessonDto lessonDto)
    {
        var session = await WorkModel.SessionRepository
            .GetFirstAsync(s => s.Id == sessionId, "Lessons");
        if (session is null)
            return NotFound(new { message = "Session not found" });

        var lesson = new Lesson
        {
            Name = lessonDto.Name,
            Type = lessonDto.Type
        };

        switch (lessonDto.Type)
        {
            case LessonType.Video when lessonDto.VideoFile is null:
                return BadRequest("Video file is required");
            case LessonType.Video:
            {
                var file = await DriveService.UploadFile(lessonDto.VideoFile);
                lesson.Video = new Video
                {
                    FileId = file.id,
                    Name = lessonDto.VideoFile.FileName,
                    DurationInMinutes = file.durationInMin
                };
                break;
            }
            case LessonType.Document when lessonDto.DocumentFiles is null || !lessonDto.DocumentFiles.Files.Any():
                return BadRequest("Document files are required");
            case LessonType.Document:
            {
                var files = new List<Document>();
                foreach (var file in lessonDto.DocumentFiles.Files)
                {
                    var res = await DriveService.UploadFile(file);
                    files.Add(new Document
                    {
                        FileId = res.id,
                        Name = file.FileName
                    });
                }

                lesson.Documents = files;
                break;
            }
            case LessonType.Quiz when lessonDto.Questions is null || !lessonDto.Questions.Any():
                return BadRequest("Quiz is required");
            case LessonType.Quiz:
                lesson.Questions = lessonDto.Questions
                    .Select(q => new Question
                    {
                        Text = q.Text,
                        Answers = q.Answers.Select(a => new Answer
                        {
                            Text = a.Text,
                            IsCorrect = a.IsCorrect
                        }).ToList()
                    }).ToList();
                break;
            default:
                return BadRequest("Specify lesson type");
        }

        session.Lessons.Add(lesson);

        await WorkModel.SessionRepository.UpdateAsync(session);
        await WorkModel.SaveAsync();

        return Ok(lesson);
    }

    /// <summary>
    /// Add lesson with quiz
    /// </summary>
    /// <param name="sessionId"></param>
    /// <param name="lessonDto"></param>
    /// <response code="200">Returns added lesson</response>
    /// <response code="404">If session not found</response>
    /// <response code="400">If lesson type is not quiz or quiz is not specified</response>
    [HttpPost("{sessionId:int}")]
    [ActionName("add-lesson-with-quiz")]
    public async Task<IActionResult> AddQuiz([FromRoute] int sessionId, [FromBody] LessonDto lessonDto)
    {
        if (lessonDto.Type != LessonType.Quiz)
            return BadRequest("Specify lesson type as quiz");

        if (lessonDto.Questions is null || !lessonDto.Questions.Any())
            return BadRequest("Quiz is required");

        var session = await WorkModel.SessionRepository
            .GetFirstAsync(s => s.Id == sessionId, "Lessons");
        if (session is null)
            return NotFound(new { message = "Session not found" });

        var lesson = new Lesson
        {
            Name = lessonDto.Name,
            Type = lessonDto.Type,
            Questions = lessonDto.Questions
                .Select(q => new Question
                {
                    Text = q.Text,
                    Answers = q.Answers.Select(a => new Answer
                    {
                        Text = a.Text,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                }).ToList()
        };

        session.Lessons.Add(lesson);

        await WorkModel.SessionRepository.UpdateAsync(session);
        await WorkModel.SaveAsync();

        return Ok(lesson);
    }

    /// <summary>
    /// Get next lesson for student
    /// </summary>
    /// <param name="courseId"></param>
    /// <param name="quizDto"></param>
    /// <response code="200">Returns next lesson</response>
    /// <response code="404">If course or student not found</response>
    [HttpPost("{courseId:int}")]
    [ActionName("next")]
    public async Task<IActionResult> NextLesson([FromRoute] int courseId, [FromBody] QuizDto? quizDto)
    {
        var course = await WorkModel.CourseRepository
            .GetFirstAsync(c => c.Id == courseId,
                "Sessions.Lessons.Questions.Answers",
                "StudentCourses.CompletedLessons.QuizAnswers",
                "StudentCourses.Lessons");

        var student = await WorkModel.UserRepository
            .GetFirstAsync(u => u.Email == User.Identity!.Name);

        if (course is null || student is null)
            return NotFound("Course or student not found");

        var enrollment = course.StudentCourses
            .FirstOrDefault(s => s.StudentId == student.Id && s.CourseId == course.Id);

        if (enrollment is null)
            return NotFound("Enrollment not found");

        var lesson = course.Sessions
            .FirstOrDefault(c => c.Id == enrollment.CurrentSessionId)?
            .Lessons.FirstOrDefault(l => l.Id == enrollment.CurrentLessonId);

        if (lesson is null)
            return NotFound("Lesson not found");

        if (lesson.Type == LessonType.Quiz && quizDto is not null)
        {
            var totalQuestions = lesson.Questions!.Count;
            var correctAnswers = (from lessonQuestion in lesson.Questions
                    let question = quizDto.QuizAnswers.FirstOrDefault(q => q.QuestionId == lessonQuestion.Id)
                    where question is not null
                    select lessonQuestion.Answers.FirstOrDefault(a => a.Id == question.AnswerId)?.IsCorrect ?? false)
                .Count(correct => correct);

            var completedLesson = new CompletedLesson
            {
                LessonId = lesson.Id,
                TestPercentage = correctAnswers * 100 / totalQuestions,
                QuizAnswers = quizDto.QuizAnswers.Select(q => new QuizAnswers
                {
                    QuestionId = q.QuestionId,
                    AnswerId = q.AnswerId
                }).ToList()
            };

            enrollment.CompletedLessons.Add(completedLesson);
        }
        else
            enrollment.Lessons.Add(lesson);

        var nextLesson = course.Sessions
            .FirstOrDefault(c => c.Id == enrollment.CurrentSessionId)?
            .Lessons.OrderBy(l => l.Id).SkipWhile(l => l.Id <= lesson.Id)
            .FirstOrDefault();

        if (nextLesson is null)
        {
            var nextSession = course.Sessions.OrderBy(s => s.Id)
                .SkipWhile(s => s.Id <= enrollment.CurrentSessionId)
                .FirstOrDefault();
            if (nextSession is null)
            {
                enrollment.Progress = 100;

                enrollment.CurrentSessionId = course.Sessions.First().Id;
                enrollment.CurrentLessonId = course.Sessions.First().Lessons.First().Id;

                await WorkModel.StudentCourseRepository.UpdateAsync(enrollment);
                await WorkModel.SaveAsync();

                return Ok(new { navigate = "/view-course/" + courseId });
            }

            enrollment.CurrentSessionId = nextSession.Id;
            enrollment.CurrentLessonId = nextSession.Lessons.MinBy(l => l.Id)?.Id ?? 0;
        }
        else
            enrollment.CurrentLessonId = nextLesson.Id;

        var totalLessons = course.Sessions.Sum(s => s.Lessons.Count);
        enrollment.Progress = enrollment.CompletedLessons.Count * 100 / totalLessons;

        await WorkModel.StudentCourseRepository.UpdateAsync(enrollment);
        await WorkModel.SaveAsync();

        return Ok();
    }

    /// <summary>
    /// Update lesson
    /// </summary>
    /// <param name="lessonId"></param>
    /// <param name="lessonDto"></param>
    /// <response code="200">Returns updated lesson</response>
    /// <response code="404">If lesson not found</response>
    /// <response code="400">If lesson type is not specified</response>
    [HttpPut("{lessonId:int}")]
    [ActionName("update-lesson")]
    public async Task<IActionResult> UpdateLesson([FromRoute] int lessonId, [FromForm] LessonDto lessonDto)
    {
        var includedProp = lessonDto.Type switch
        {
            LessonType.Document => "Documents",
            LessonType.Quiz => "Questions.Answers",
            LessonType.Video => "Video",
            _ => null
        };

        if (includedProp is null)
            return BadRequest("specify lesson type");

        var lesson = await WorkModel.LessonRepository
            .GetFirstAsync(s => s.Id == lessonId, includedProp);
        if (lesson is null)
            return NotFound(new { message = "Lesson not found" });

        switch (lessonDto.Type)
        {
            case LessonType.Video when lessonDto.VideoFile is null:
                return BadRequest("Video file is required");
            case LessonType.Video:
            {
                await DriveService.DeleteFile(lesson.Video!.FileId);

                var file = await DriveService.UploadFile(lessonDto.VideoFile);
                lesson.Video = new Video
                {
                    FileId = file.id,
                    Name = lessonDto.VideoFile.FileName,
                    DurationInMinutes = file.durationInMin
                };
                break;
            }
            case LessonType.Document when lessonDto.DocumentFiles is null || !lessonDto.DocumentFiles.Files.Any():
                return BadRequest("Document files are required");
            case LessonType.Document:
            {
                foreach (var file in lessonDto.DocumentFiles.Files)
                {
                    var res = await DriveService.UploadFile(file);
                    lesson.Documents?.Add(new Document
                    {
                        FileId = res.id,
                        Name = file.FileName
                    });
                }

                break;
            }
            case LessonType.Quiz when lessonDto.Questions is null || !lessonDto.Questions.Any():
                return BadRequest("Quiz is required");
            case LessonType.Quiz:

                foreach (var questionDto in lessonDto.Questions)
                {
                    lesson.Questions?.Add(new Question
                    {
                        Text = questionDto.Text,
                        Answers = questionDto.Answers.Select(answerDto => new Answer
                        {
                            Text = answerDto.Text,
                            IsCorrect = answerDto.IsCorrect
                        }).ToList()
                    });
                }

                break;
            default:
                return BadRequest("Specify lesson type");
        }

        lesson.Name = lessonDto.Name;

        await WorkModel.LessonRepository.UpdateAsync(lesson);
        await WorkModel.SaveAsync();

        return Ok(lesson);
    }

    /// <summary>
    /// Adds new questions to lesson
    /// </summary>
    /// <param name="lessonId"></param>
    /// <param name="questions"></param>
    /// <response code="200">Returns updated lesson</response>
    [HttpPut("{lessonId:int}")]
    [ActionName("update-quiz")]
    public async Task<IActionResult> UpdateQuiz([FromRoute] int lessonId, [FromBody] ICollection<QuestionDto> questions)
    {
        var lesson = await WorkModel.LessonRepository
            .GetFirstAsync(s => s.Id == lessonId, "Questions.Answers");

        if (lesson?.Type is not LessonType.Quiz)
            return NotFound("Lesson not found");

        if (!questions.Any())
            return BadRequest("Quiz is required");

        foreach (var questionDto in questions)
        {
            lesson.Questions?.Add(new Question
            {
                Text = questionDto.Text,
                Answers = questionDto.Answers.Select(answerDto => new Answer
                {
                    Text = answerDto.Text,
                    IsCorrect = answerDto.IsCorrect
                }).ToList()
            });
        }

        await WorkModel.LessonRepository.UpdateAsync(lesson);
        await WorkModel.SaveAsync();

        return Ok(lesson);
    }

    /// <summary>
    /// Delete lesson
    /// </summary>
    /// <param name="lessonId"></param>
    /// <response code="200">Returns deleted lesson</response>
    [HttpDelete("{lessonId:int}")]
    [ActionName("delete-lesson")]
    public async Task<IActionResult> DeleteLesson([FromRoute] int lessonId)
    {
        await WorkModel.LessonRepository.DeleteAsync(lessonId);
        await WorkModel.SaveAsync();

        return Ok("Lesson deleted");
    }

    /// <summary>
    /// Delete course
    /// </summary>
    /// <param name="courseId"></param>
    /// <response code="200">Returns deleted course</response>
    [HttpDelete("{courseId:int}")]
    [ActionName("delete-course")]
    public async Task<IActionResult> DeleteCourse([FromRoute] int courseId)
    {
        await WorkModel.CourseRepository.DeleteAsync(courseId);
        await WorkModel.SaveAsync();

        return Ok("Course deleted");
    }

    /// <summary>
    /// Delete session
    /// </summary>
    /// <param name="sessionId"></param>
    /// <response code="200">Returns deleted session</response>
    [HttpDelete("{sessionId:int}")]
    [ActionName("delete-session")]
    public async Task<IActionResult> DeleteSession([FromRoute] int sessionId)
    {
        await WorkModel.SessionRepository.DeleteAsync(sessionId);
        await WorkModel.SaveAsync();

        return Ok("Session deleted");
    }
}
