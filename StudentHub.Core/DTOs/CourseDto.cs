using Microsoft.AspNetCore.Http;

namespace StudentHub.Core.DTOs;

public class CourseDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int? Price { get; set; }
    public int CategoryId { get; set; }
    public IFormFile? Image { get; set; }
}
