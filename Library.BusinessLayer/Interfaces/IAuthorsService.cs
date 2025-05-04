using Library.BusinessLayer.DTOs.Author;
using Library.Models;

namespace Library.BusinessLayer.Interfaces
{
    public interface IAuthorsService
    {
        Task<IEnumerable<Author>> GetAll();
        Task<Author> Get(int id);
        Task<Author> Create(CreatAuthorRequestDto authorDto);
        Task<Author> Update(int id, UpdateAuthorRequestDto updateAuthorRequestDto);
        Task Delete(int id);
    }
}
