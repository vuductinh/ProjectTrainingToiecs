using MessagePack;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTrainingToiecs.Models
{
    public class Users
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ? UserName { get; set; }
        public string ? Password { get; set; }
        public string ? Email { get; set; }
        public string ? Phone { get; set; }
        public bool Active { get; set; }
        public int RoleId { get; set; }
        public int TypeCourse { get; set; }
        public string ? FullName { get; set; }
        public int RecordStatusId { get; set; }
        public DateTime? Created { get; set; }
        [NotMapped]
        public int Process { get; set; }
        [NotMapped]
        public string ActiveTxt
        {
            get
            {
                return Active ? "Hoạt động" : "Chưa kích hoạt";
            }
        }
        [NotMapped]
        public int Order { get; set; }
        [NotMapped]
        public string TypeTxt
        {
            //get
            //{
            //    return TypeCourse == 1 ? "Cơ bản" : "Nâng cao";
            //}
            get;set;
        }
        [NotMapped]
        public string ProcessColor
        {
            get
            {
                var val = "";
                if(Process > 0 && Process < 30)
                {
                    val = "text-danger";
                }
                else if (Process >= 30 && Process < 65)
                {
                    val = "text-info";
                }else if(Process >= 60 && Process <= 100)
                {
                    val = "text-success";
                }
                return val;
            }
        }
        [NotMapped]
        public string StartDate
        {
            get
            {
                return Created.HasValue ? Created.Value.ToString("dd/MM/yyyy") : "";
            }
        }
        [NotMapped]
        public string ColorCourse
        {
            get
            {
                var val = "";
                if (TypeTxt == "Beginner")
                {
                    val = "#520DC2";
                }
                else if (TypeTxt == "Advence")
                {
                    val = "#B02A37";
                }
                else if (TypeTxt == "Intermeditate")
                {
                    val = "#CC9A06";
                }
                return val;
            }
        }
        [NotMapped]
        public bool MemberNew
        {
            get
            {
                return Created.HasValue ? ((DateTime.Now - Created.Value).TotalDays <= 7 ? true : false) : false;
            }
        }
    }
}
