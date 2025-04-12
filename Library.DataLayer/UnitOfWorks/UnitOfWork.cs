using Library.Interfaces;
using Library.Data;
using Library.Models;
using Library.DataLayer.Repository;
using Library.DataLayer.Interfaces;

namespace Library.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryContext _context;
        private IGenericRepository<Book> _bookRepository;
        private IAuthorRepository _authorRepository;


        public UnitOfWork(LibraryContext context, 
                          IGenericRepository<Book> bookRepository,
                          IAuthorRepository authorRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public IGenericRepository<Book> BookRepository => _bookRepository ??= new GenericRepository<Book>(_context);
        public IAuthorRepository AuthorRepository => _authorRepository ??= new AuthorRepository(_context);


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
