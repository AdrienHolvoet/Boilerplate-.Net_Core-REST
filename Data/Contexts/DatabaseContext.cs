using Boilerplate.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Data.Contexts
{

    public class DatabaseContext : DbContext
    {
        public DbSet<Author> authors { get; set; }
        public DbSet<Book> books { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }
    }
}