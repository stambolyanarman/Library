using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Library.Models;

namespace Library.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(b => b.Books)
                .WithOne(a => a.Author)
                .HasForeignKey(a => a.AuthorId);
        }
    }
}
