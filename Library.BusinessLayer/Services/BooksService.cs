using Library.BusinessLayer.DTOs.Book;
using Library.BusinessLayer.Interfaces;
using Library.BusinessLayer.Mappers;
using Library.DTOs.Book;
using Library.Models;
using Library.UnitOfWorks;

namespace Library.BusinessLayer.Services
{
    public class BooksService : IBooksService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BookDto>> GetBooks()
        {
            var books = await _unitOfWork.BookRepository.GetAllAsync();
            var bookDto = books.Select(x => x.ToBookDto());
            return bookDto;
        }

        public async Task<Book> GetBook(int id)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return null;
            }
            return book;
        }

        public async Task<Book> CreateBook(int authorId, CreatBookRequestDto creatDto)
        {
            if (!await _unitOfWork.AuthorRepository.AuthorExistsAsync(authorId))
            {
                return null;
            }
            var book = creatDto.ToBookFromCreatDto(authorId);
            await _unitOfWork.BookRepository.CreatAsync(book);
            await _unitOfWork.SaveAsync();
            return book;
        }

        public async Task<BookDto> UpdateBook(int id,UpdateBookRequestDto updateDto)
        {
            var book = await _unitOfWork.BookRepository.UpdateAsync(id, updateDto.ToBookFromUpdateDto());
            await _unitOfWork.SaveAsync();
            if (book == null)
            {
                return null;
            }
            return book.ToBookDto();
        }

        public async Task DeleteBook(int id)
        {
            await _unitOfWork.BookRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
