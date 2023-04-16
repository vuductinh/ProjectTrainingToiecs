using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTrainingToiecs.Models
{
    public class BaseCommon
    {
        [NotMapped]
        public bool PosiontFirst { get; set; }
        [NotMapped]
        public bool PosiontLast { get; set; }
        [NotMapped]
        public int IdNext { get; set; }
        [NotMapped]
        public int IdPre { get; set; }
        [NotMapped]
        public int TotalQuesion { get; set; }
        [NotMapped]
        public string? OptionResult { get; set; }
    }
}
