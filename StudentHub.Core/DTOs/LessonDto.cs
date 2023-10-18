using DataAccess.Models;
using Microsoft.AspNetCore.Http;

namespace StudentHub.Core.DTOs;

public class LessonDto
{
    public string Name { get; set; } = null!;
    public LessonType Type { get; set; }

    public IFormCollection? DocumentFiles { get; set; }
    public IFormFile? VideoFile { get; set; }

    public ICollection<QuestionDto>? Questions { get; set; }
}
