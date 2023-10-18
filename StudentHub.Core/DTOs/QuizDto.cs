namespace StudentHub.Core.DTOs;

public class QuizDto
{
    public ICollection<QuizAnswersDto> QuizAnswers { get; set; } = new List<QuizAnswersDto>();
}

public class QuizAnswersDto
{
    public int QuestionId { get; set; }
    public int AnswerId { get; set; }
}
