using Microsoft.AspNetCore.Mvc;
using StudentHub.Core.DTOs;
using StudentHub.Core.WorkModel;

namespace StudentHub.Controllers;

/// <summary>
/// Filter controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FilterController : Controller
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="workModel"></param>
    public FilterController(IUnitOfWork workModel)
    {
        WorkModel = workModel;
    }

    private IUnitOfWork WorkModel { get; }

    /// <summary>
    /// Get all categories
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categories = await WorkModel.CategoryRepository.GetAsync();
        return Ok(categories);
    }

    /// <summary>
    /// Filter courses
    /// </summary>
    /// <param name="filter"></param>
    /// <response code="200">Returns filtered courses</response>
    [HttpPost]
    public async Task<IActionResult> Filter([FromBody] FilterDto filter)
    {
        var courses = await WorkModel.CourseRepository
            .GetAsync(null, null, "Teacher");

        if (filter.Certification is not null && filter.Certification.Value)
            courses = courses.Where(c => c.Certification);

        if (filter.Certification is not null && !filter.Certification.Value)
            courses = courses.Where(c => !c.Certification);

        if (filter.Free is not null && filter.Free.Value)
            courses = courses.Where(c => c.Price is 0 or null);

        if (filter.Free is not null && !filter.Free.Value)
            courses = courses.Where(c => c.Price is not 0 and not null);

        if (filter.CategoryId is not null)
            courses = courses.Where(c => c.CategoryId == filter.CategoryId);

        return Ok(courses);
    }
}
