using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Books
    {
        public int Id { get; set; }
        [MaxLength(15)]
        public string Title { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }
        public int PublicationYear { get; set; }
    }
}
