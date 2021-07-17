using Boilerplate.Data.Models;
using Boilerplate_REST.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate_REST.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }
    }
}
