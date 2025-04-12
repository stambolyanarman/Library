using Library.Data;
using Library.DataLayer.Interfaces;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.DataLayer.Repository
{
    public class AuthorRepository : GenericRepository<Author>,IAuthorRepository 
    {
        private readonly LibraryContext _context;
        public AuthorRepository(LibraryContext context): 
        base(context)
        {
            _context = context;
        }
        public async Task<bool> AuthorExistsAsync(int authorId)
        {
            return await _context.Authors.AnyAsync(x => x.Id == authorId);
        }
    }
}
