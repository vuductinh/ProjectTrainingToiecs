using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTrainingToiecs.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string? QS { get; set; }
        public string? QSA { get; set; }
        public string? QSB { get; set; }
        public string? QSC { get; set; }
        public string? QSD { get; set; }
        public string? CorrectAnswer { get; set; }
        public int IdQS { get; set; }
        public int RecordStatusId { get; set; }
        public int ItemOrder { get; set; }
        public string? Description { get; set; }
        [NotMapped]
        public bool Marked { get; set; }
        [NotMapped]
        public string? Option { get; set; }
        [NotMapped]
        public bool ResultOption
        {
            get
            {
                var val = false;
                if (Marked && Option == "A")
                {
                    val = true;
                }
                else if (Marked && Option == "B")
                {
                    val = true;
                }
                else if (Marked && Option == "C")
                {
                    val = true;
                }
                else if (Marked && Option == "D")
                {
                    val = true;
                }
                return val;
            }
        }
        [NotMapped]
        public bool ResultTxt
        {
            get
            {
                var val = false;
                if (Marked && CorrectAnswer == "A")
                {
                    val = true;
                }
                else if (Marked && CorrectAnswer == "B")
                {
                    val = true;
                }
                else if (Marked && CorrectAnswer == "C")
                {
                    val = true;
                }
                else if (Marked && CorrectAnswer == "D")
                {
                    val = true;
                }
                return val;
            }
        }
        [NotMapped]
        public bool SatusTxt
        {
            get
            {
                var val = false;
                if (Marked && CorrectAnswer == "A" && CorrectAnswer == Option)
                {
                    val = true;
                }
                else if (Marked && CorrectAnswer == "B" && CorrectAnswer == Option)
                {
                    val = true;
                }
                else if (Marked && CorrectAnswer == "C" && CorrectAnswer == Option)
                {
                    val = true;
                }
                else if (Marked && CorrectAnswer == "D" && CorrectAnswer == Option)
                {
                    val = true;
                }
                return val;
            }
        }
        [NotMapped]
        public bool OptionTxt
        {
            get
            {
                var val = false;
                if (Marked && CorrectAnswer == "A" && CorrectAnswer != Option)
                {
                    val = true;
                }
                else if (Marked && CorrectAnswer == "B" && CorrectAnswer != Option)
                {
                    val = true;
                }
                else if (Marked && CorrectAnswer == "C" && CorrectAnswer != Option)
                {
                    val = true;
                }
                else if (Marked && CorrectAnswer == "D" && CorrectAnswer != Option)
                {
                    val = true;
                }
                return val;
            }
        }

    }
}
