using Boilerplate_REST.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate_REST.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Author> authors { get; set; }
        public DbSet<Book> books { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }
    }
}