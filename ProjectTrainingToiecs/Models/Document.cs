using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTrainingToiecs.Models
{
    public class Document
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string? Image { get; set; }
        public string ? Audio { get; set; }
        public string? Text { get; set; }
        public string ?Title { get; set; }
        public int RecordStatusId { get; set; }
        [NotMapped]
        public int Order { get; set; }  
    }
}
