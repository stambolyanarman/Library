using Library.DTOs.Book;

namespace Library.DTOs.Author
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<BookDto> Books { get; set; }
    }
}
