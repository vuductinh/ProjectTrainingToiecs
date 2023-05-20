namespace ProjectTrainingToiecs.Models
{
    public class StatusStudy
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Option { get; set; }
        public bool Result { get; set; }
        public int IdTest { get; set; }
        public int UnitId { get; set; }
    }
}
