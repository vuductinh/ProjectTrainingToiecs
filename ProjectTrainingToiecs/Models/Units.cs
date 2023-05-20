using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTrainingToiecs.Models
{
    public class Units
    {
        public int Id { get; set; }
        public string ?Name { get; set; }
        public string ? Prosess { get; set; }
        public string ? Description { get; set; }
        public DateTime? LastUpdate { get; set; }
        public int Count { get; set; }
        public int CourseId { get; set; }
        [NotMapped]
        public int Order { get; set; }
        [NotMapped]
        public string? Type { get; set; }
        public int RecordStatusId { get; set; }
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
                    if (diffOfDates.TotalDays > 7)
                    {
                        val = LastUpdate.Value.ToString("dd/MM/yyyy");
                    }
                    else if (diffOfDates.Days <= 7 && diffOfDates.Days > 0)
                    {
                        val = String.Format("{0} days ago", diffOfDates.Days);
                    }
                    else if (diffOfDates.Days == 0 && diffOfDates.Hours > 1)
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
        [NotMapped]
        public DateTime? LastSeen { get; set; }
        [NotMapped]
        public string LastSeenTxt
        {
            get
            {
                var val = "";
                if (LastSeen.HasValue)
                {
                    var dateTime = DateTime.Now;
                    var diffOfDates = dateTime.Subtract(LastSeen.Value);
                    if (diffOfDates.TotalDays > 7)
                    {
                        val = LastSeen.Value.ToString("dd/MM/yyyy");
                    }
                    else if (diffOfDates.Days <= 7 && diffOfDates.Days > 0)
                    {
                        val = String.Format("{0} days ago", diffOfDates.Days);
                    }
                    else if (diffOfDates.Days == 0 && diffOfDates.Hours >= 1)
                    {
                        val = String.Format("{0} hours ago", diffOfDates.Hours);
                    }
                    else if (diffOfDates.Hours == 0 && diffOfDates.Minutes >= 1)
                    {
                        val = String.Format("{0} minutes ago", diffOfDates.Minutes);
                    }
                    else if (diffOfDates.Minutes == 0 && diffOfDates.Seconds >= 1)
                    {
                        val = String.Format("{0} seconds ago", diffOfDates.Seconds);
                    }
                }
                return val;
            }
        }
        [NotMapped]
        public int Marks { get; set; }
        [NotMapped]
        public int Process
        {
            get
            {
                return Count > 0 ? Convert.ToInt32((Marks * 100) / Count) : 0;
            }
        }
        [NotMapped]
        public string ProcessTxt { get {
                string val = "";
                if (Process > 0 && Process <= 30)
                {
                    val = "#fc0002";
                }
                else if (Process > 30 && Process <= 70)
                {
                    val = "#ff8a00";
                }
                else if(Process > 70)
                {
                    val = "#28A745";
                }
                return val;
            } }
    }
}
