using Library.BusinessLayer.DTOs.Book;
using Library.DTOs.Book;
using Library.Models;


namespace Library.BusinessLayer.Interfaces
{
    public interface IBooksService
    {
        Task<IEnumerable<BookDto>> GetBooks();
        Task<Book> GetBook(int id);
        Task<Book> CreateBook(int authorId, CreatBookRequestDto creatDto);
        Task<BookDto> UpdateBook(int id, UpdateBookRequestDto updateDto);
        Task DeleteBook(int id);
    }
}
