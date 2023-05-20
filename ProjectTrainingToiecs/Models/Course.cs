using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTrainingToiecs.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Process { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        [NotMapped]
        public int Order { get; set; }
        public int RecordStatusId { get; set; }
        public DateTime? LastUpdate { get; set; }
        [NotMapped]
        public string LastUpdateTxt
        {
            get
            {
                var val = "";
                if (LastUpdate.HasValue)
                {
                    var dateTime = DateTime.Now;
                    var diffOfDates = dateTime.Subtract(LastUpdate.Value);
                    if(diffOfDates.TotalDays > 7)
                    {
                        val = LastUpdate.Value.ToString("dd/MM/yyyy");
                    }else if(diffOfDates.Days <= 7 && diffOfDates.Days > 0)
                    {
                        val = String.Format("{0} days ago", diffOfDates.Days);
                    }else if(diffOfDates.Days == 0 && diffOfDates.Hours > 1)
                    {
                        val = String.Format("{0} hours ago", diffOfDates.Hours);
                    }
                    else if (diffOfDates.Hours == 0 && diffOfDates.Minutes > 1)
                    {
                        val = String.Format("{0} minutes ago", diffOfDates.Minutes);
                    }
                    else if (diffOfDates.Minutes == 0 && diffOfDates.Seconds > 1)
                    {
                        val = String.Format("{0} seconds ago", diffOfDates.Seconds);
                    }
                }
                return val;
            }
        }
    }
}
