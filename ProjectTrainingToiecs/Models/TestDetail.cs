using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTrainingToiecs.Models
{
    public class TestDetail : BaseCommon
    {
        public int Id { get; set; }
        public string? AnswerA { get; set; }
        public string? AnswerB { get; set; }
        public string? AnswerC { get; set; }
        public string? AnswerD { get; set; }
        public string? CorrectAnswer { get; set; }
        public int IdTest { get; set; }
        public string? Description { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public int Type { get; set; }
        public int DocumentId { get; set; }
        public string? LinkImage { get; set; }
        public string? Audio { get; set; }
        public int ItemOrder { get; set; }
        public string? Title { get; set; }
        [NotMapped]
        public int Order { get; set; }
        [NotMapped]
        public string? NameLesson { get; set; }
        public int RecordStatusId { get; set; }
        public string? Scription { get; set; }
        public int UnitId { get; set; }
        public int TypeDetail { get; set; }
        [NotMapped]
        public List<Question>? QuestionsDetail { get; set; }
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
                if(Marked && Option == "A")
                {
                    val = true;
                }else if (Marked && Option == "B")
                {
                    val = true;
                }else if (Marked && Option == "C")
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
