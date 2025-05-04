using Library.BusinessLayer.DTOs.Book;

namespace Library.BusinessLayer.DTOs.Author
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<BookDto>? Books { get; set; }
    }
}
