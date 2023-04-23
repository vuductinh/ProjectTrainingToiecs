namespace ProjectTrainingToiecs.Models
{
    public class UserExpDate
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime ExpDate { get; set; }
        public DateTime SignDate { get; set; }
        public int RecordStatusId { get; set; }
    }
}
