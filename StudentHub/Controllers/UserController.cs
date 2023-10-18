using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Core.DTOs;
using StudentHub.Core.Interfaces;
using StudentHub.Core.WorkModel;

namespace StudentHub.Controllers;

/// <summary>
/// Controller for user actions
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : Controller
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="workModel"></param>
    /// <param name="driveService"></param>
    public UserController(IUnitOfWork workModel, IDriveService driveService)
    {
        WorkModel = workModel;
        DriveService = driveService;
    }

    private IUnitOfWork WorkModel { get; }
    private IDriveService DriveService { get; }

    /// <summary>
    /// Returns user's role
    /// </summary>
    /// <response code="200">Returns user's role</response>
    /// <response code="404">User not found</response>
    [HttpGet]
    [ActionName("me")]
    public async Task<IActionResult> GetMe()
    {
        var user = await WorkModel.UserRepository.GetFirstAsync(u => u.Email == User.Identity!.Name);
        if (user is null)
            return NotFound("User not found");

        return Ok(new { role = user.Role });
    }

    /// <summary>
    /// Returns user's data
    /// </summary>
    /// <response code="200">Returns user's data</response>
    [HttpGet]
    [ActionName("data")]
    public async Task<IActionResult> GetData()
    {
        var data = await WorkModel.DataRepository
            .GetAsync();

        var res = data
            .GroupBy(d => d.Type)
            .ToDictionary(d => d.Key, d => d.ToList());

        return Ok(res);
    }

    /// <summary>
    /// Returns user's details
    /// </summary>
    /// <response code="200">Returns user's details</response>
    /// <response code="404">User not found</response>
    [HttpGet]
    [ActionName("details")]
    public async Task<IActionResult> GetDetails()
    {
        var user = await WorkModel.UserRepository
            .GetFirstAsync(u => u.Email == User.Identity!.Name);

        if (user is null)
            return NotFound("User not found");

        return Ok(user);
    }

    /// <summary>
    /// Updates user's picture
    /// </summary>
    /// <param name="picture"></param>
    /// <response code="200">Returns user's picture</response>
    /// <response code="404">User not found</response>
    [HttpPost]
    [ActionName("picture")]
    public async Task<IActionResult> UpdatePicture([FromForm] IFormFile picture)
    {
        var user = await WorkModel.UserRepository
            .GetFirstAsync(u => u.Email == User.Identity!.Name);

        if (user is null)
            return NotFound("User not found");

        var res = await DriveService.UploadFile(picture);
        user.PhotoId = res.id;

        await WorkModel.UserRepository.UpdateAsync(user);
        await WorkModel.SaveAsync();

        return Ok(res.id);
    }

    /// <summary>
    /// Updates user's details
    /// </summary>
    /// <param name="user"></param>
    /// <response code="200">Returns user's details</response>
    /// <response code="404">User not found</response>
    [HttpPost]
    [ActionName("details")]
    public async Task<IActionResult> UpdateDetails([FromBody] UserDetails user)
    {
        var oldUser = await WorkModel.UserRepository
            .GetFirstAsync(u => u.Email == User.Identity!.Name);

        if (oldUser is null)
            return NotFound("User not found");

        if (user.FullName is not null && user.FullName.Contains(' '))
        {
            oldUser.FirstName = user.FullName.Split(" ")[0];
            oldUser.LastName = user.FullName.Split(" ")[1];
        }

        if (user.PhoneNumber is not null)
            oldUser.PhoneNumber = user.PhoneNumber;

        if (user.Day is not null && user.Month is not null && user.Year is not null)
            oldUser.BirthDate = new DateTime(user.Year.Value, user.Month.Value, user.Day.Value);

        if (user.NewOpportunities is not null)
            oldUser.NewOpportunities = user.NewOpportunities.Value;

        if (user.GetDiploma is not null)
            oldUser.GetDiploma = user.GetDiploma.Value;

        if (user.Skills is not null)
            oldUser.Skills = user.Skills;

        if (user.Aim is not null)
            oldUser.Aim = user.Aim;

        if (user.Occupation is not null)
            oldUser.Occupation = user.Occupation;

        if (user.WantedLevel is not null)
            oldUser.WantedLevel = user.WantedLevel;

        if (user.About is not null)
            oldUser.About = user.About;

        if (user.Location is not null)
            oldUser.Location = user.Location;

        if (user.Sex is not null)
            oldUser.Sex = user.Sex;

        await WorkModel.UserRepository.UpdateAsync(oldUser);
        await WorkModel.SaveAsync();

        return Ok(oldUser);
    }

    /// <summary>
    /// Returns user's courses
    /// </summary>
    /// <response code="200">Returns user's courses</response>
    /// <response code="404">User not found</response>
    [HttpGet]
    [ActionName("courses")]
    public async Task<IActionResult> GetCourses()
    {
        var student = await WorkModel.StudentRepository
            .GetFirstAsync(u => u.Email == User.Identity!.Name, "StudentCourses.Course.Teacher");

        if (student is null)
            return NotFound("User not found");

        return Ok(student.StudentCourses.Where(sc => sc.Paid));
    }
}
