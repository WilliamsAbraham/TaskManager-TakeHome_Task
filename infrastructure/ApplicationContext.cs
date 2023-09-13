using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure
{
    public class ApplicationContext:DbContext
    {
        string _connectionString = "Server=localhost;Database=Taskmanager;Uid=root;Pwd=Mypospas;";
        public ApplicationContext(DbContextOptions<ApplicationContext> optons):base(optons)
        {
            
        }
        public ApplicationContext()
        {
                
        }

        public DbSet<User> Users { get; set; }
        public DbSet<MyProject> Projects { get; set; }
        public DbSet<MyTask> MyTasks { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (options.IsConfigured)
            {
                return;
            }

            options.UseMySQL(_connectionString);
        }
    }
}
