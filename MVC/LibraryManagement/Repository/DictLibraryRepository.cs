using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Repository
{
    public class DictLibraryRepository: ILibraryRepository
    {
       
        Dictionary<int, Books> _books = new Dictionary<int, Books>
        {
            { 1, new Books { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Price = 10, PublicationYear = 1925 } },
            { 2, new Books { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Price = 12, PublicationYear = 1960 } },
            { 3, new Books { Id = 3, Title = "1984", Author = "George Orwell", Price = 15, PublicationYear = 1949 } }
        };
        public void AddBook(Books book)
        {
            int newId = _books.Keys.Max() + 1;
            book.Id = newId;
            _books.Add(newId, book);
        }
        public List<Books> GetAllBooks()
        {
            return _books.Values.ToList();
        }
        [HttpGet]
        public List<Books> GetBooksByPrice(int price)
        {
            return _books.Values.Where(b => b.Price >= price).ToList();
        }
        [HttpGet]
        public int BookByName(string name)
        {
            return _books.Count(b => b.Value.Title.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
