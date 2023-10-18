using Newtonsoft.Json;

namespace DataAccess.Models;

public class Video
{
    public int Id { get; set; }

    public string FileId { get; set; } = null!;
    public string Name { get; set; } = null!;
    [JsonProperty("duration")] public long? DurationInMinutes { get; set; }

    public int LessonId { get; set; }
    public Lesson Lesson { get; set; } = null!;
}
