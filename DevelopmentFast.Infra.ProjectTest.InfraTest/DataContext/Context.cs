using DevelopmentFast.Infra.ProjectTest.DomainTest.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevelopmentFast.Infra.ProjectTest.InfraTest.DataContext
{
    public class Context : DbContext
    {
        public DbSet<Student> Student { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }


    }
}
