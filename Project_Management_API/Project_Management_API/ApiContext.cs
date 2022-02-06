using Microsoft.EntityFrameworkCore;
using Project_Management_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = Project_Management_API.Model.Task;

namespace Project_Management_API
{
    public class ApiContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Model.Task> ProjectTasks { get; set; }

        public ApiContext(DbContextOptions options) : base(options)
        {
        }
    }
}
