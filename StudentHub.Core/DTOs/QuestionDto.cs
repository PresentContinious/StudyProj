namespace StudentHub.Core.DTOs;

public class QuestionDto
{
    public string Text { get; set; } = null!;

    public ICollection<AnswerDto> Answers { get; set; } = null!;
}

public class AnswerDto
{
    public string Text { get; set; } = null!;
    public bool IsCorrect { get; set; }
}
