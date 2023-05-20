namespace ProjectTrainingToiecs.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string? AdressAccess { get; set; }
        public string? PageAccess { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
