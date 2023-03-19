namespace ProjectTrainingToiecs.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Prosess { get; set; }
        public string? Description { get; set; }

        public int UnitId { get; set; }
    }
}
