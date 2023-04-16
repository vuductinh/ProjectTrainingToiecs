using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTrainingToiecs.Models
{
    public class TestDetail : BaseCommon
    {
        public int Id { get; set; }
        public string? AnswerA { get; set; }
        public string? AnswerB { get; set; }
        public string? AnswerC { get; set; }
        public string? AnswerD { get; set; }
        public string? CorrectAnswer { get; set; }
        public int IdTest { get; set; }
        public string? Description { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public int Type { get; set; }
        public int DocumentId { get; set; }
        public string? LinkImage { get; set; }
        public string? Audio { get; set; }
        public int ItemOrder { get; set; }
        public string? Title { get; set; }
        [NotMapped]
        public int Order { get; set; }
        [NotMapped]
        public string? NameLesson { get; set; }
    }
}
