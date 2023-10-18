using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Core.WorkModel;

namespace StudentHub.Controllers;

/// <summary>
/// Controller for payment actions
/// </summary>
[ApiController]
[Authorize(Roles = "Student")]
[Route("api/[action]/{courseId:int}")]
public class PaymentController : Controller
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="workModel"></param>
    public PaymentController(IUnitOfWork workModel)
    {
        WorkModel = workModel;
    }

    private IUnitOfWork WorkModel { get; }

    /// <summary>
    /// Checks if student can pay for course
    /// </summary>
    /// <param name="courseId"></param>
    /// <response code="200">Returns course</response>
    /// <response code="400">If student has already paid for this course</response>
    /// <response code="404">If student or course not found</response>
    [HttpGet]
    [ActionName("check")]
    public async Task<IActionResult> Check([FromRoute] int courseId)
    {
        var student = await WorkModel.StudentRepository
            .GetFirstAsync(u => u.Email == User.Identity!.Name, "StudentCourses");

        var course = await WorkModel.CourseRepository
            .GetFirstAsync(c => c.Id == courseId, "Sessions.Lessons");

        if (student is null || course is null)
            return NotFound("Student or course not found");

        if (student.StudentCourses.Any(sc => sc.StudentId == student.Id
                                             && sc.CourseId == course.Id && sc.Paid))
            return BadRequest("You have already paid for this course");

        if (course.Price is not (null or 0))
            return Ok(course);

        var studentCourse = new StudentCourse
        {
            Course = course,
            Paid = true,
            CurrentLessonId = course.Sessions.First().Lessons.First().Id,
            CurrentSessionId = course.Sessions.First().Id,
            Progress = 0,
            Stars = 0,
            Review = null,
            PaymentMethodId = null
        };

        student.StudentCourses.Add(studentCourse);

        await WorkModel.StudentRepository.UpdateAsync(student);
        await WorkModel.SaveAsync();

        return Ok(false);
    }

    /// <summary>
    /// Pays for course
    /// </summary>
    /// <param name="courseId"></param>
    /// <param name="method"></param>
    /// <response code="200">Returns course id</response>
    /// <response code="400">If student has already paid for this course</response>
    /// <response code="404">If student or course not found</response>
    [HttpPost]
    [ActionName("pay")]
    public async Task<IActionResult> Pay([FromRoute] int courseId, [FromBody] PaymentMethod method)
    {
        var student = await WorkModel.StudentRepository
            .GetFirstAsync(u => u.Email == User.Identity!.Name, "StudentCourses");

        var course = await WorkModel.CourseRepository
            .GetFirstAsync(c => c.Id == courseId, "Sessions.Lessons");

        if (student is null || course is null)
            return NotFound("Student or course not found");

        if (student.StudentCourses.Any(sc => sc.StudentId == student.Id
                                             && sc.CourseId == course.Id && sc.Paid))
            return BadRequest("You have already paid for this course");

        method.Email ??= student.Email;
        var studentCourse = new StudentCourse
        {
            Course = course,
            Paid = true,
            CurrentLessonId = course.Sessions.First().Lessons.First().Id,
            CurrentSessionId = course.Sessions.First().Id,
            Progress = 0,
            Stars = 0,
            Review = null,
            PaymentMethod = method
        };

        student.StudentCourses.Add(studentCourse);

        await WorkModel.StudentRepository.UpdateAsync(student);
        await WorkModel.SaveAsync();

        return Ok(course.Id);
    }
}
