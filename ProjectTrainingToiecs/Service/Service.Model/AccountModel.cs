namespace ProjectTrainingToiecs.Service.Service.Model
{
    public class AccountModel
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string ? Password { get; set; }
        public string ? Email { get; set; }
        public bool Active { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public int TypeCourse { get; set; }
    }
}
