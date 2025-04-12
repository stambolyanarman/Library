using Library.Interfaces;
using Library.Models;

namespace Library.DataLayer.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<bool> AuthorExistsAsync(int authorId);

    }
}
