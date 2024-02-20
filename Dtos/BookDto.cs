using LibApp.Models;

namespace LibApp.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Author { get; set; } = String.Empty;
        public Genre Genre { get; set; }

        public DateTime DataAdded { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int NumberInStock { get; set; }
    }
}
