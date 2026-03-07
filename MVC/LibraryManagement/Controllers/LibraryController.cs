using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Repository;
using LibraryManagement.Models;
namespace LibraryManagement.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryRepository repo;

        public LibraryController(ILibraryRepository repository)
        {
            repo = repository;
        }
        [HttpGet]
        public IActionResult AddBook(string title, int price, string author)
        {
            var book = new Books
            {
                Title = title,
                Price = price,
                Author = author
            };
            repo.AddBook(book);
            return Content($"Book '{title}' added successfully.");
        }
        public IActionResult Index()
        {
            var books = repo.GetAllBooks();

            List<string> result = new List<string>();

            foreach (var s in books)
            {
                result.Add($"{s.Title} {s.Price} {s.Author}");
            }

            return Content(string.Join("\n", result));
        }
         public IActionResult GetBooksByPrice(int price)
        {
            var books = repo.GetBooksByPrice(price);

            List<string> result = new List<string>();

            foreach (var s in books)
            {
                result.Add($"{s.Title} {s.Price} {s.Author}");
            }

            return Content(string.Join("\n", result));
        }
         public IActionResult BookByName(string name)
        {
            var count = repo.BookByName(name);
            return Content($"Number of books with name '{name}': {count}");
        }
    }
}
