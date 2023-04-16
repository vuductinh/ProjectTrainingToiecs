using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTrainingToiecs.Models
{
    public class Units
    {
        public int Id { get; set; }
        public string ?Name { get; set; }
        public string ? Prosess { get; set; }
        public string ? Description { get; set; }

        public int CourseId { get; set; }
        [NotMapped]
        public int Order { get; set; }
        [NotMapped]
        public string Type { get; set; }
    }
}
