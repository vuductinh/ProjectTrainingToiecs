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
    }
