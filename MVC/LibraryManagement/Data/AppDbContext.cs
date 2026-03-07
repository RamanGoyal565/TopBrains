using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Books> Books { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Books>().ToTable("BookMaster");
            modelBuilder.Entity<Books>().HasData(
               new Books { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Price = 10, PublicationYear = 1925 },
               new Books { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Price = 12, PublicationYear = 1960 },
               new Books { Id = 3, Title = "1984", Author = "George Orwell", Price = 15, PublicationYear = 1949 }
           );
        }
    }
}