using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectTrainingToiecs.Models;

    public class DbTrainingToiecsContext : DbContext
    {
        public DbTrainingToiecsContext (DbContextOptions<DbTrainingToiecsContext> options)
            : base(options)
        {
        }

        public DbSet<ProjectTrainingToiecs.Models.Users> Users { get; set; } = default!;
        public DbSet<ProjectTrainingToiecs.Models.Course> Course { get; set; } = default!;
        public DbSet<ProjectTrainingToiecs.Models.Units> Units { get; set; } = default!;
        public DbSet<ProjectTrainingToiecs.Models.Lesson> Lesson { get; set; } = default!;
        public DbSet<ProjectTrainingToiecs.Models.Document> Documents { get; set; } = default!;
        public DbSet<ProjectTrainingToiecs.Models.TestDetail> TestDetails { get; set; } = default!;
        public DbSet<ProjectTrainingToiecs.Models.StatusStudy> StatusStudies { get; set; } = default!;
}
