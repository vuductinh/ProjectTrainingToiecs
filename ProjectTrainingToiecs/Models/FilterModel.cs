namespace ProjectTrainingToiecs.Models
{
    public class FilterModel
    {
        public int PageSize { get; set; }
        public string? TextSearch { get; set; }
        public int TypeId { get; set; }
        public int CourseId { get; set; }
        public int UnitId { get; set; }
        public int TypeQuesion { get; set; }
        public int TypeQuesionDetail { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }
}
