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
            get
            {
                return TypeCourse == 1 ? "Cơ bản" : "Nâng cao";
            }
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
    }
}
