﻿using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
