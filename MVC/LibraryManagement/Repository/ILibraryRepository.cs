using LibraryManagement.Models;
namespace LibraryManagement.Repository
{
    public interface ILibraryRepository
    {
        void AddBook(Books book);
        List<Books> GetAllBooks();
        List<Books> GetBooksByPrice(int price);
        int BookByName(string name);
    }
}