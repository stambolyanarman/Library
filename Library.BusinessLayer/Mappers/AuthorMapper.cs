using Library.DTOs.Author;
using Library.Models;

namespace Library.Mappers
{
    public static class AuthorMapper
    {
        public static AuthorDto ToAuthorDto(this Author author )
        {
            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Books = author.Books.Select(c => c.ToBookDto()).ToList()
            };
        }
        public static Author ToAuthorFromCreatDto(this CreatAuthorRequestDto authorDto)
        {
            return new Author
            {
                Name = authorDto.Name
            };
        }
        public static Author ToAuthorFromUpdateDto(this UpdateAuthorRequestDto authorDto)
        {
            return new Author
            {
                Name = authorDto.Name
            };
        }
    }
}
