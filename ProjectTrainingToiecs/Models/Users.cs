using MessagePack;
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
        public string ? FullName { get; set; }
    }
}
