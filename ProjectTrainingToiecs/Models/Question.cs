namespace ProjectTrainingToiecs.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string? Answer { get; set; }
        public string? CorrectAnswer { get; set; }
        public int IdLesson { get; set; }
        public int RecordStatusId { get; set; }

    }
}
