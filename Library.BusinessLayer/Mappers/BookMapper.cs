using Library.BusinessLayer.DTOs.Book;
using Library.Models;

namespace Library.BusinessLayer.Mappers
{
    public static class BookMapper
    {
        public static BookDto ToBookDto(this Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                AuthorId = book.AuthorId,                
            };
        }
        public static Book ToBookFromCreatDto(this CreatBookRequestDto bookDto, int authorId)
        {
            return new Book
            {
                Title = bookDto.Title,
                AuthorId = authorId 
            };
        }
        public static Book ToBookFromUpdateDto(this UpdateBookRequestDto bookDto)
        {
            return new Book
            {
                Title = bookDto.Title,
                AuthorId = bookDto.AuthorId
            };
        }
    }
}
