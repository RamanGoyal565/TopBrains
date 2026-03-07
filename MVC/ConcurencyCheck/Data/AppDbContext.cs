using ConcurencyCheck.Models;
using Microsoft.EntityFrameworkCore;
namespace ConcurencyCheck.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, Name = "Amit Sharma", Salary = 45000 },
                new Employee { EmployeeId = 2, Name = "Priya Singh", Salary = 52000 },
                new Employee { EmployeeId = 3, Name = "Rahul Verma", Salary = 60000 }
            );
        }
    }
}