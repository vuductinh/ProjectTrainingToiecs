using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTrainingToiecs.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Prosess { get; set; }
        public string? Description { get; set; }

        public int UnitId { get; set; }
        [NotMapped]
        public int Order { get; set; }
        [NotMapped]
        public string? NameUnit { get; set; }
        public int RecordStatusId { get; set; }
    }
}
