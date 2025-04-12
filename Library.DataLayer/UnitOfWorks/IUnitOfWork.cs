using Library.DataLayer.Interfaces;
using Library.Interfaces;
using Library.Models;

namespace Library.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IGenericRepository<Book> BookRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        Task SaveAsync();
        void Dispose();
    }
}
