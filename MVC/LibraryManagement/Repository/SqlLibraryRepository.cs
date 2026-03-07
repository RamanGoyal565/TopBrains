using LibraryManagement.Models;
using LibraryManagement.Data;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Repository
{
    public class SqlLibraryRepository: ILibraryRepository
    {
        private readonly AppDbContext _context;
        
        public void AddBook(Books book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }
        public SqlLibraryRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Books> GetAllBooks()
        {
            return _context.Books.ToList();
        }
        [HttpGet]
        public List<Books> GetBooksByPrice(int price)
        {
            return _context.Books.Where(b => b.Price >= price).ToList();
        }
        public int BookByName(string name)
        {
            return _context.Books.Count(b => b.Title.ToLower() == name.ToLower());
        }
    }
}
