using Microsoft.EntityFrameworkCore;
using Library.Configurations;
using Library.Models;

namespace Library.Data
{
    public class LibraryContext(DbContextOptions<LibraryContext> options) 
        : DbContext(options)
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Book> Books { get; set; } 
        public DbSet<Author> Authors { get; set; }
    }
}
